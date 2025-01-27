using UnityEngine;

public class Ring : MonoBehaviour
{
    public int RingID { get; private set; }
    [SerializeField] private float height = 0.5f; // Высота кольца для позиционирования
    [SerializeField] private float scaleFactor = 1.2f; // Множитель размера для визуального отличия колец
    private int size; // Размер кольца (1 - самое маленькое, N - самое большое)
    private Color color; // Цвет кольца
    private Material material; // Материал для изменения цвета
    private Peg currentPeg; // Колонка, на которой находится кольцо

    public float Height => height; // Высота кольца
    public int Size => size; // Размер кольца
    public Peg CurrentPeg => currentPeg; // Текущая колонка

    public void Initialize(int ringID)
    {
        RingID = ringID;
    }
    
    public void SetColor(Color newColor)
    {
        color = newColor;
        if (material == null)
        {
            // Получаем материал кольца (создаем новый, чтобы не изменять оригинальный материал префаба)
            Renderer renderer = GetComponent<Renderer>();
            material = new Material(renderer.material);
            renderer.material = material;
        }
        material.color = color; // Применяем цвет к материалу
    }

    /// <summary>
    /// Устанавливает размер кольца.
    /// </summary>
    public void SetSize(int newSize)
    {
        size = newSize;
        // Масштабируем кольцо в зависимости от размера
        float scale = 1f + (size - 1) * scaleFactor;
        transform.localScale = new Vector3(scale, height, scale); // Увеличиваем диаметр, сохраняя высоту
    }

    /// <summary>
    /// Устанавливает текущую колонку, на которой находится кольцо.
    /// </summary>
    public void SetCurrentPeg(Peg peg)
    {
        currentPeg = peg;
    }

    /// <summary>
    /// Проверяет, является ли кольцо верхним на своей колонке.
    /// </summary>
    public bool IsTopRing()
    {
        if (currentPeg == null) return false;
        return currentPeg.TopRing == this;
    }
}
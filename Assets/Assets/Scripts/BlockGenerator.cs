using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks; // Массив префабов блоков
    private GameObject currentBlock; // Текущий активный блок
    private GameObject lastBlock; // Последний сгенерированный блок
    public GameObject player; // Игрок
    private bool isGeneratingNextBlock = false; // Флаг для препятсвия многократной генерации нового блока

    void Start()
    {
        // Генерация первого блока при старте
        GenerateInitialBlock();
    }

    void Update()
    {
        // Проверка на игрока рядом с блоком
        if (currentBlock != null && !isGeneratingNextBlock)
        {
            Transform finishDoor = currentBlock.transform.Find("FinishDoor");
            if (finishDoor != null)
            {
                float distanceToFinishDoor = Vector3.Distance(player.transform.position, finishDoor.position);

                // Если игрок близко к финишной двери, генерируем следующий блок
                if (distanceToFinishDoor < 10f)
                {
                    DeleteBlock();
                    isGeneratingNextBlock = true;
                    GenerateNextBlock(finishDoor);
                }
            }
        }
    }

    void DeleteBlock()
    {

        if (lastBlock != null)
        {
            Destroy(lastBlock);
            lastBlock = null;
        }
            
        
    }

    // Генерация первого блока
    void GenerateInitialBlock()
    {
        currentBlock = Instantiate(blocks[0]);
        Transform startDoor = currentBlock.transform.Find("StartDoor");
        if (startDoor != null)
        {
            // Перемещаем игрока к стартовой двери
            player.transform.position = startDoor.position + Vector3.up * 1.5f; // Сдвиг вверх, чтобы игрок стоял над полом
        }
    }

    // Генерация следующего блока
    public void GenerateNextBlock(Transform finishDoor)
    {
        // Выбор случайного блока
        int randomIndex = Random.Range(0, blocks.Length);
        GameObject nextBlock = Instantiate(blocks[randomIndex]);

        Transform nextStartDoor = nextBlock.transform.Find("StartDoor");
        if (nextStartDoor != null)
        {
            // Поворот нового блока
            nextBlock.transform.rotation = finishDoor.rotation;

            Vector3 doorOffset = nextStartDoor.position - nextBlock.transform.position; // Смещение стартовой двери относительно нового блока
            nextBlock.transform.position = finishDoor.position - doorOffset; // Позиция блока в нужное место
                                  
            // Блокируем стартовую дверь нового блока
            BoxCollider startDoorCollider = finishDoor.GetComponent<BoxCollider>();
            if (startDoorCollider != null)
            {
                startDoorCollider.enabled = true;
                Debug.Log("Коллайдер стартовой двери отключен.");
            }
        }
        else
        {
            Debug.LogError("Стартовая дверь не найдена в новом блоке!");
        }       

        // Обновление глобальных переменных
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        isGeneratingNextBlock = false;
    }
}
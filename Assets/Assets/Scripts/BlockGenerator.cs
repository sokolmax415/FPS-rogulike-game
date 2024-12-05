using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks; // Массив префабов блоков
    private GameObject currentBlock; // Текущий активный блок
    private GameObject lastBlock; // Последний сгенерированный блок
    public GameObject player; // Игрок
    private bool isGeneratingNextBlock = false; // Флаг для препятсвия многократной генерации нового блока
    //private bool new_level = false;
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
                if (distanceToFinishDoor < 5f)
                {
                    isGeneratingNextBlock = true;
                    //new_level = true;
                    DeleteBlock();                    
                    DiactivateCollider(finishDoor);
                    GenerateNextBlock(finishDoor);
                }
            }
        }
        /*Transform startDoor = currentBlock.transform.Find("StartDoor");
        if (startDoor != null)
        {
            float distanceToStarthDoor = Vector3.Distance(player.transform.position, startDoor.position);
            if (distanceToStarthDoor > 5f && new_level)
            {
                Debug.Log("fjfjfff");
                ActivateCollider(startDoor);
                new_level = false;
            }
        } */

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
        Transform finishDoor = currentBlock.transform.Find("FinishDoor");
        if (startDoor != null)
        {
            // Перемещаем игрока к стартовой двери
            player.transform.position = startDoor.position - startDoor.forward * 1.5f;
        }
    }

    // Генерация следующего блока
    public void GenerateNextBlock(Transform finishDoor)
    {
        // Выбор случайного блока
        int randomIndex = Random.Range(0, blocks.Length);
        GameObject nextBlock = Instantiate(blocks[randomIndex]);

        Transform nextStartDoor = nextBlock.transform.Find("StartDoor");
        //Transform nextFinishDoor = nextBlock.transform.Find("FinishDoor");
        if (nextStartDoor != null)
        {
            // Поворот нового блока
            nextBlock.transform.rotation = finishDoor.rotation;

            Vector3 doorOffset = nextStartDoor.position - nextBlock.transform.position; // Смещение стартовой двери относительно нового блока
            nextBlock.transform.position = finishDoor.position - doorOffset; // Позиция блока в нужное место
            DiactivateCollider(nextStartDoor);
           
            
        }      
        // Обновление глобальных переменных
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        isGeneratingNextBlock = false;
    }
    
    // Включение коллайдера двери
    void ActivateCollider(Transform door)
    {
        BoxCollider DoorCollider = door.GetComponent<BoxCollider>();
        if (DoorCollider != null)
        {
            DoorCollider.enabled = true;
        }
    }

    // Выключение коллайдера двери
    void DiactivateCollider(Transform door)
    {
        BoxCollider DoorCollider = door.GetComponent<BoxCollider>();
        if (DoorCollider != null)
        {
            DoorCollider.enabled = false;
        }
    }
}
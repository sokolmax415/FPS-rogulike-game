using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks; // ������ �������� ������
    private GameObject currentBlock; // ������� �������� ����
    private GameObject lastBlock; // ��������� ��������������� ����
    public GameObject player; // �����
    private bool isGeneratingNextBlock = false; // ���� ��� ���������� ������������ ��������� ������ �����

    void Start()
    {
        // ��������� ������� ����� ��� ������
        GenerateInitialBlock();
    }

    void Update()
    {
        // �������� �� ������ ����� � ������
        if (currentBlock != null && !isGeneratingNextBlock)
        {
            Transform finishDoor = currentBlock.transform.Find("FinishDoor");
            if (finishDoor != null)
            {
                float distanceToFinishDoor = Vector3.Distance(player.transform.position, finishDoor.position);

                // ���� ����� ������ � �������� �����, ���������� ��������� ����
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

    // ��������� ������� �����
    void GenerateInitialBlock()
    {
        currentBlock = Instantiate(blocks[0]);
        Transform startDoor = currentBlock.transform.Find("StartDoor");
        if (startDoor != null)
        {
            // ���������� ������ � ��������� �����
            player.transform.position = startDoor.position + Vector3.up * 1.5f; // ����� �����, ����� ����� ����� ��� �����
        }
    }

    // ��������� ���������� �����
    public void GenerateNextBlock(Transform finishDoor)
    {
        // ����� ���������� �����
        int randomIndex = Random.Range(0, blocks.Length);
        GameObject nextBlock = Instantiate(blocks[randomIndex]);

        Transform nextStartDoor = nextBlock.transform.Find("StartDoor");
        if (nextStartDoor != null)
        {
            // ������� ������ �����
            nextBlock.transform.rotation = finishDoor.rotation;

            Vector3 doorOffset = nextStartDoor.position - nextBlock.transform.position; // �������� ��������� ����� ������������ ������ �����
            nextBlock.transform.position = finishDoor.position - doorOffset; // ������� ����� � ������ �����
                                  
            // ��������� ��������� ����� ������ �����
            BoxCollider startDoorCollider = finishDoor.GetComponent<BoxCollider>();
            if (startDoorCollider != null)
            {
                startDoorCollider.enabled = true;
                Debug.Log("��������� ��������� ����� ��������.");
            }
        }
        else
        {
            Debug.LogError("��������� ����� �� ������� � ����� �����!");
        }       

        // ���������� ���������� ����������
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        isGeneratingNextBlock = false;
    }
}
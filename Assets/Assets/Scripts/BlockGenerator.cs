using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks; // Ìàññèâ ïðåôàáîâ áëîêîâ
    private GameObject currentBlock; // Òåêóùèé àêòèâíûé áëîê
    private GameObject lastBlock; // Ïîñëåäíèé ñãåíåðèðîâàííûé áëîê
    public GameObject player; // Èãðîê
    private bool isGeneratingNextBlock = false; // Ôëàã äëÿ ïðåïÿòñâèÿ ìíîãîêðàòíîé ãåíåðàöèè íîâîãî áëîêà
    //private bool new_level = false;
    void Start()
    {
        // Ãåíåðàöèÿ ïåðâîãî áëîêà ïðè ñòàðòå
        GenerateInitialBlock();
    }

    void Update()
    {
        // Ïðîâåðêà íà èãðîêà ðÿäîì ñ áëîêîì
        if (currentBlock != null && !isGeneratingNextBlock)
        {
            Transform finishDoor = currentBlock.transform.Find("FinishDoor");
            
            if (finishDoor != null)
            {
                float distanceToFinishDoor = Vector3.Distance(player.transform.position, finishDoor.position);

                // Åñëè èãðîê áëèçêî ê ôèíèøíîé äâåðè, ãåíåðèðóåì ñëåäóþùèé áëîê
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

    // Ãåíåðàöèÿ ïåðâîãî áëîêà
    void GenerateInitialBlock()
    {
        currentBlock = Instantiate(blocks[0]);
        Transform startDoor = currentBlock.transform.Find("StartDoor");
        Transform finishDoor = currentBlock.transform.Find("FinishDoor");
        if (startDoor != null)
        {
            // Ïåðåìåùàåì èãðîêà ê ñòàðòîâîé äâåðè
            player.transform.position = startDoor.position - startDoor.forward * 1.5f;
            player.transform.position = new Vector3(player.transform.position.x, startDoor.position.y + 1.4f, player.transform.position.z);
        }
    }

    // Ãåíåðàöèÿ ñëåäóþùåãî áëîêà
    public void GenerateNextBlock(Transform finishDoor)
    {
        // Âûáîð ñëó÷àéíîãî áëîêà
        int randomIndex = Random.Range(0, blocks.Length);
        GameObject nextBlock = Instantiate(blocks[randomIndex]);

        Transform nextStartDoor = nextBlock.transform.Find("StartDoor");
        //Transform nextFinishDoor = nextBlock.transform.Find("FinishDoor");
        if (nextStartDoor != null)
        {
            // Ïîâîðîò íîâîãî áëîêà
            nextBlock.transform.rotation = finishDoor.rotation;

            Vector3 doorOffset = nextStartDoor.position - nextBlock.transform.position; // Ñìåùåíèå ñòàðòîâîé äâåðè îòíîñèòåëüíî íîâîãî áëîêà
            nextBlock.transform.position = finishDoor.position - doorOffset; // Ïîçèöèÿ áëîêà â íóæíîå ìåñòî
            DiactivateCollider(nextStartDoor);
           
            
        }      
        // Îáíîâëåíèå ãëîáàëüíûõ ïåðåìåííûõ
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        isGeneratingNextBlock = false;
    }
    
    // Âêëþ÷åíèå êîëëàéäåðà äâåðè
    void ActivateCollider(Transform door)
    {
        BoxCollider DoorCollider = door.GetComponent<BoxCollider>();
        if (DoorCollider != null)
        {
            DoorCollider.enabled = true;
        }
    }

    // Âûêëþ÷åíèå êîëëàéäåðà äâåðè
    void DiactivateCollider(Transform door)
    {
        BoxCollider DoorCollider = door.GetComponent<BoxCollider>();
        if (DoorCollider != null)
        {
            DoorCollider.enabled = false;
        }
    }
}

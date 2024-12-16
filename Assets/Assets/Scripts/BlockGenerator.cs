using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks;
    private GameObject currentBlock;
    private GameObject lastBlock;
    public GameObject player;
    private bool isGeneratingNextBlock = false;
    void Start()
    {
        GenerateInitialBlock();
    }

    void Update()
    {
        if (currentBlock != null && !isGeneratingNextBlock)
        {
            Transform finishDoor = currentBlock.transform.Find("FinishDoor");
            
            if (finishDoor != null)
            {
                float distanceToFinishDoor = Vector3.Distance(player.transform.position, finishDoor.position);

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

    void GenerateInitialBlock()
    {
        currentBlock = Instantiate(blocks[0]);
        Transform startDoor = currentBlock.transform.Find("StartDoor");
        Transform finishDoor = currentBlock.transform.Find("FinishDoor");
        if (startDoor != null)
        {
            player.transform.position = startDoor.position - startDoor.forward * 1.5f;
            player.transform.position = new Vector3(player.transform.position.x, startDoor.position.y + 1f, player.transform.position.z);
        }
    }

    public void GenerateNextBlock(Transform finishDoor)
    {
        int randomIndex = Random.Range(0, blocks.Length);
        GameObject nextBlock = Instantiate(blocks[randomIndex]);

        Transform nextStartDoor = nextBlock.transform.Find("StartDoor");
        //Transform nextFinishDoor = nextBlock.transform.Find("FinishDoor");
        if (nextStartDoor != null)
        {
            nextBlock.transform.rotation = finishDoor.rotation;

            Vector3 doorOffset = nextStartDoor.position - nextBlock.transform.position;
            nextBlock.transform.position = finishDoor.position - doorOffset;
            DiactivateCollider(nextStartDoor);
           
            
        }
        lastBlock = currentBlock;
        currentBlock = nextBlock;
        isGeneratingNextBlock = false;
    }
    
    void ActivateCollider(Transform door)
    {
        BoxCollider DoorCollider = door.GetComponent<BoxCollider>();
        if (DoorCollider != null)
        {
            DoorCollider.enabled = true;
        }
    }

    void DiactivateCollider(Transform door)
    {
        BoxCollider DoorCollider = door.GetComponent<BoxCollider>();
        if (DoorCollider != null)
        {
            DoorCollider.enabled = false;
        }
    }
}

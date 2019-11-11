using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoardManager : MonoBehaviour
{
    public int maxRows, maxColumns;
    public int minRoom;

    private GameObject[,] boardPositionsFloor;
    public GameObject floorTile;
    public GameObject wallTile;

    public class SubDungeon
    {
        public Rect rect;
        private static int count = 0;
        public Rect room = new Rect(-1, -1, 0, 0);
        public int debug;
        private bool splitH;

        public SubDungeon left, right;


        public SubDungeon(Rect rect)
        {
            this.rect = rect;
            debug = count;
            count++;
        }

        public bool IAmLeaf()
        {
            return left == null && right == null;
        }

        public bool Split(int minRoom)
        {
            if (!IAmLeaf() )
            {
                return false;
            }

            

            if (rect.width > minRoom)
            {
                splitH = false;
            }
            else if (rect.height > minRoom)
            {
                splitH = true;
            }


         

            if (splitH)
            {
                int split = (int)(rect.height / 2);
                left = new SubDungeon(new Rect(rect.x, rect.y, rect.width, split));
                right = new SubDungeon(new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
            }
            else
            {
                int split = (int)(rect.width / 2);

                left = new SubDungeon(new Rect(rect.x, rect.y, split, rect.height));
                right = new SubDungeon(new Rect(rect.x + split, rect.y, rect.width - split, rect.height));
            }

            return true;


        }

        public void CreateRoom()
        {
            if (left != null)
            {
                left.CreateRoom();
            }
            if (right != null)
            {
                
                right.CreateRoom();
            }


            if (IAmLeaf())
            {
                int roomWidth = (int)(rect.width-1);
                int roomHeight = (int)(rect.height-1);
                int roomX = 1;
                int roomY = 1;
                // room position will be absolute in the board, not relative to the sub-dungeon
                room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
                //Debug.Log("Created room " + room + " in sub-dungeon " + debugId + " " + rect);
            }
        }



        public Rect GetRoom()
        {
            if (IAmLeaf())
            {
                Debug.Log(room);
                return room;
            }
            if (left != null)
            {
                Rect lroom = left.GetRoom();
                if (lroom.x != -1)
                {
                    return lroom;
                }
            }
            if (right != null)
            {
                Rect rroom = right.GetRoom();
                if (rroom.x != -1)
                {
                    return rroom;
                }
            }

            return new Rect(-1, -1, 0, 0);
        }

    }

    public void CreateBSP(SubDungeon subDungeon)
    {
       // Debug.Log("Splitting sub-dungeon " + subDungeon.debug + ": " + subDungeon.rect);
        if (subDungeon.IAmLeaf())
        {
            if (subDungeon.rect.width > minRoom || subDungeon.rect.height > minRoom)
            {
                if (subDungeon.Split(minRoom))
                {
                   // Debug.Log("Splitted sub-dungeon " + subDungeon.debug + " in "
                   //   + subDungeon.left.debug + ": " + subDungeon.left.rect + ", "
                   // + subDungeon.right.debug + ": " + subDungeon.right.rect);

                    CreateBSP(subDungeon.left);
                    CreateBSP(subDungeon.right);
                }
            }
        }
    }

    public void DrawRooms(SubDungeon subDungeon)
    {
        if (subDungeon == null)
        {
            return;
        }
        if (subDungeon.IAmLeaf())
        {
            for (int i = (int)subDungeon.room.x ; i < subDungeon.room.xMax; i++)
            {
                for (int j = (int)subDungeon.room.y ; j < subDungeon.room.yMax; j++)
                {
                    GameObject instance = Instantiate(floorTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                    boardPositionsFloor[i, j] = instance;

                }
            }
        }
        else
        {
            DrawRooms(subDungeon.left);
            DrawRooms(subDungeon.right);
        }

    }

    public void DrawWalls(SubDungeon subDungeon)
    {
        if (subDungeon == null)
        {
            return;
        }
        if (subDungeon.IAmLeaf())
        {
            for (int i = (int)subDungeon.room.x - 1; i < subDungeon.room.xMax +1 ; i++)
            {
                for (int j = (int)subDungeon.room.y - 1; j < subDungeon.room.yMax +1 ; j++)
                {
                    if (i == subDungeon.room.x - 1 || j == subDungeon.room.y - 1 || i == subDungeon.room.xMax || j == subDungeon.room.yMax)
                    {
                        if (boardPositionsFloor[i, j] == null)
                        {

                            GameObject wall = Instantiate(wallTile, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                            wall.transform.SetParent(transform);
                            boardPositionsFloor[i, j] = wall;
                        }

                    }
                }
            }
        }
        else
        {
            DrawWalls(subDungeon.left);
            DrawWalls(subDungeon.right);
        }
    }

    private void Start()
    {
        SubDungeon subdungeon = new SubDungeon(new Rect(0, 0, maxRows, maxColumns));
        CreateBSP(subdungeon);
        subdungeon.CreateRoom();

        boardPositionsFloor = new GameObject[maxRows+1, maxColumns+1];
        DrawRooms(subdungeon);
        DrawWalls(subdungeon);
    }
       
}

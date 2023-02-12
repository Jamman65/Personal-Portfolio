using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerStats playerstats;

        public List<GameObject> LeftLegModels;
        string LeftLegName = "Left_Leg_Model_1";
        public GameObject LeftLeg;

        public List<GameObject> helmetModels;
        string HelmetModelName = "Helmet_Model_1";
        public GameObject helmet;


        public List<GameObject> RightLegModels;
        string RightLegName = "Right_Leg_Model_1";

        public List<GameObject> HipModels;
        string hipModelName = "Hip_Model_1";

        public List<GameObject> HandModels;
        public GameObject RightHand;
        public GameObject LeftHand;

        public GameObject hip;
        public GameObject RightLeg;
        public GameObject torso;

        HelmetModelChanger helmetModelChanger;
        TorsoModelChanger torsoModelChanger;
        //HipModelChanger hipModelChanger;
        //LeftLegModelChanger leftLegModelChanger;
        //RightLegModelChanger rightLegModelChanger;
        public BlockingCollder blockingCollider;


        public GameObject DefaultHeadModel;
        public GameObject DefaultTorsoModel;
        public GameObject DefaultHipModel;
        public GameObject DefaultRightLeg;
        public GameObject DefaultLeftLeg;
        public GameObject DefaultRightHand;
        public GameObject DefaultLeftHand;

        private void Awake()
        {
            GetAllLeftLegModels();
            GetAllRightLegModels();
            GetAllHipModels();



            inputHandler = GetComponentInParent<InputHandler>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            playerstats = GetComponentInParent<PlayerStats>();
            //hipModelChanger = GetComponentInChildren<HipModelChanger>();
            //leftLegModelChanger = GetComponentInChildren<LeftLegModelChanger>();
            //rightLegModelChanger = GetComponentInChildren<RightLegModelChanger>();
            //  blockingCollider = GetComponentInChildren<BlockingCollder>();
        }

        private void Start()
        {
            EquipAllEquipment();
            EquipLegs();
            EquipHands();

        }

        private void EquipAllEquipment()
        {
            HideAllHelmetModels();

            if (playerInventory.currentHelmetEquipment != null)
            {
                DefaultHeadModel.SetActive(false);
                helmet.SetActive(true);
                EquipHelmetModel(playerInventory.currentHelmetEquipment.helmetModelName);
                playerstats.HelmetDefense = playerInventory.currentHelmetEquipment.physicalDefense;
                Debug.Log("Helmet Defense = " + playerstats.HelmetDefense);

            }
            else
            {
                //equip default helmet
                DefaultHeadModel.SetActive(true);
                playerstats.HelmetDefense = 0;
            }



            torsoModelChanger.HideAllTorsoModels();

            if (playerInventory.currentTorsoEquipment != null)
            {
                DefaultTorsoModel.SetActive(false);
                torso.SetActive(true);
                torsoModelChanger.EquipTorsoModel(playerInventory.currentTorsoEquipment.TorsoModelName);
                playerstats.ChestDefense = playerInventory.currentTorsoEquipment.physicalDefense;
                Debug.Log("Torso Defense = " + playerstats.ChestDefense);
            }
            else
            {
                //equip default torso
                DefaultTorsoModel.SetActive(true);
                playerstats.ChestDefense = 0;
            }

            //Equip Hip and leg equipment

            //HideAllHipModels();
            //HideAllLeftLegModels();
            //HideAllRightLegModels();

            //if (playerInventory.currentLegEquipment != null)
            //{
            //   // GetAllLeftLegModels();

            //    DefaultHipModel.SetActive(false);
            //    DefaultLeftLeg.SetActive(false);
            //    DefaultRightLeg.SetActive(false);




            //    EquipHipModel(playerInventory.currentLegEquipment.hipModelName);
            //    Debug.Log("HELLO");
            //    EquipLeftLegModels(playerInventory.currentLegEquipment.leftLegName);
            //    EquipRightLegModel(playerInventory.currentLegEquipment.rightLegName);
            //}
            //else
            //{
            //    Debug.Log("YOOOOOOOOOOO");

            //    DefaultHipModel.SetActive(true);
            //    DefaultLeftLeg.SetActive(true);
            //    DefaultRightLeg.SetActive(true);
            //}

            //Equip Left Arms and hands

            //Equip right arms and hands


        }

        private void EquipLegs()
        {

            if (playerInventory.currentLegEquipment != null)
            {
                LeftLeg.SetActive(true);
                hip.SetActive(true);
                RightLeg.SetActive(true);

                DefaultHipModel.SetActive(false);
                playerstats.LegDefense = playerInventory.currentLegEquipment.physicalDefense;
                Debug.Log("Leg Defense " + playerstats.LegDefense);
            }
            else
            {
                DefaultLeftLeg.SetActive(true);
                DefaultRightLeg.SetActive(true);
                DefaultHipModel.SetActive(true);
                playerstats.LegDefense = 0;
            }
        }
        private void EquipHands()
        {
            if(playerInventory.currentHandEquipment != null)
            {
                RightHand.SetActive(true);
                LeftHand.SetActive(true);
                playerstats.HandDefense = playerInventory.currentHandEquipment.physicalDefense;
                Debug.Log("Hand Defense " + playerstats.HandDefense);

            }
            else
            {
                DefaultLeftHand.SetActive(true);
                DefaultRightHand.SetActive(true);
                playerstats.HandDefense = 0;
            }
        }




        #region LeftLegModels
        private void GetAllLeftLegModels()
        {
            //int childrenGameObjects = transform.childCount;

            //for (int i = 0; i < childrenGameObjects; i++)
            //{
            //    LeftLegModels.Add(transform.GetChild(i).gameObject);
            //}

            LeftLegModels.Add(GameObject.Find(LeftLegName));
        }

        public void HideAllLeftLegModels()
        {
            foreach (GameObject helmetmodel in LeftLegModels)
            {
                helmetmodel.SetActive(false);
            }
        }

        public void EquipLeftLegModels(string TorsoName)
        {
            for (int i = 0; i < LeftLegModels.Count; i++)
            {
                if (LeftLegModels[i].name == TorsoName) //finds a specifc helmet by name and equips it
                {
                    //LeftLegModels[i].SetActive(true);
                    LeftLeg.SetActive(true);
                }
            }
        }
        #endregion


        #region HelmetModels
        private void GetAllHelmetModels()
        {
            //int childrenGameObjects = transform.childCount;

            //for (int i = 0; i < childrenGameObjects; i++)
            //{
            //    helmetModels.Add(transform.GetChild(i).gameObject);
            //}

            helmetModels.Add(GameObject.Find(HelmetModelName));
        }

        public void HideAllHelmetModels()
        {
            foreach (GameObject helmetmodel in helmetModels)
            {
                helmetmodel.SetActive(false);
            }
        }

        public void EquipHelmetModel(string helmetName)
        {
            for (int i = 0; i < helmetModels.Count; i++)
            {
                if (helmetModels[i].name == helmetName) //finds a specifc helmet by name and equips it
                {
                    helmetModels[i].SetActive(true);
                }
            }
        }
        #endregion

        #region RightLegModels




        private void GetAllRightLegModels()
        {
            //int childrenGameObjects = transform.childCount;

            //for (int i = 0; i < childrenGameObjects; i++)
            //{
            //    RightLegModels.Add(transform.GetChild(i).gameObject);
            //}

            RightLegModels.Add(GameObject.Find(RightLegName));
        }

        public void HideAllRightLegModels()
        {
            foreach (GameObject helmetmodel in RightLegModels)
            {
                helmetmodel.SetActive(false);
            }
        }

        public void EquipRightLegModel(string TorsoName)
        {
            RightLeg.SetActive(true);
            for (int i = 0; i < RightLegModels.Count; i++)
            {
                if (RightLegModels[i].name == TorsoName) //finds a specifc helmet by name and equips it
                {
                    RightLegModels[i].SetActive(true);

                }
            }
        }
        #endregion

        #region HipModels


        private void GetAllHipModels()
        {
            HipModels.Add(GameObject.Find(hipModelName));
        }

        public void HideAllHipModels()
        {
            foreach (GameObject hip in HipModels)
            {
                hip.SetActive(false);
            }
        }

        public void EquipHipModel(string TorsoName)
        {
            for (int i = 0; i < HipModels.Count; i++)
            {
                if (HipModels[i].name == TorsoName) //finds a specifc helmet by name and equips it
                {
                    HipModels[i].SetActive(true);
                    // hip.SetActive(true);
                }
            }
        }
        #endregion


        public void OpenBlockingCollider()
        {
            if (inputHandler.TwoHandFlag)
            {
                blockingCollider.SetColliderBlockingDamage(playerInventory.rightWeapon);
            }
            else
            {
                blockingCollider.SetColliderBlockingDamage(playerInventory.leftWeapon);
            }


            blockingCollider.EnableBlockingCollider();
        }
    }
}

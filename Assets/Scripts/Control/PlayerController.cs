﻿using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            //155 Cursor and UI
            if (InteractWithUI()) return;

            //Neu player isDead = true thi dung lai khong lam gi nua
            //Neu InteractWithCombat = true thi tiep tuc cong viec InteractWithCombat va khong lam nhung viec khac
            //Neu InteractWithMovement = true thi tiep tuc cong viec InteractWithCombat va khong lam nhung viec khac
            if (health.IsDead())
            {
                SetCursor(CursorType.Dead);
                return;
            }
            if (InteractWithComponent()) return;
            //if (InteractWithCombat()) return;
            if(InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                IRaycastable[]raycastables =  hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRayCast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        //private bool InteractWithCombat()
        //{
        //    //Khi nhap chuot vao 1 vi tri trong khong gian 3D thi RaycastHit se xuyen qua tat ca nhung doi tuong va tra ve thong tin cua tat ca doi tuong do duoi dang 1 Array
        //    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        //    //Khi da co 1 array thi bat dau goi vong lap for de lay thong tin cua tung thanh phan
        //    foreach(RaycastHit hit in hits)
        //    {
        //        //Neu ray di qua 1 doi tuong co componentCombatTarget thi gan doi tuong do cho target
        //        CombatTarget target = hit.transform.GetComponent<CombatTarget>();
        //        //Neu khong co doi tuong nao co component target thi tiep tuc
        //        if (target == null) continue;


        //        // Ham CanAttack tra ve true neu co target va target !isDead()
        //        // Neu CanAttack tra ve false thi tiep tuc
        //        if (!GetComponent<Fighter>().CanAttack(target.gameObject))
        //        {
        //            continue;
        //        }
        //        //Neu target == null thi tiep tuc
        //        if(target == null)
        //        {
        //            continue;
        //        }
        //        //Neu nhap vao target

        //        //Ham Attack nhan vao 1 combatTarget
        //        //GetComponent ActionScheduler cua doi tuong va sau do goi ham StartAction va dua vao action hien tai
        //        //Gan target = combatTarget.GetComponent<Health>();

        //        //O day goi ham Attack cua doi tuong sau do gan target trong Fighter = target click chuot vao tu do ta co the goi tat ca cac ham trong Fighter
        //        if (Input.GetMouseButton(0))
        //        {
        //            GetComponent<Fighter>().Attack(target.gameObject);
        //        }
        //        // tra ve true neu co target
        //        SetCursor(CursorType.Combat);
        //        return true;
        //    }
        //    //retrun false when there is no combat target
        //    return false;
        //}
        // InteractWithMovement se chuyen player den vi tri click vao sau do tra ve true
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    // Replace Move to with StartMoveAction to Cancel player fighting
                    // Before moving
                    GetComponent<Mover>().StartMoveAction(hit.point,0.8f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            //
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
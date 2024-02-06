using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._1.Scripts.Player
{
    public class PlayerWallChecker: MonoBehaviour
    {
        public PlayerLayerData layerData;
        public bool IsFrontWallDetected => FrontWallCheck();
        public float RayDistance = 0.05f;
        public bool DrawGizmosRay;
        public Color RayColor;

        public Transform Orientation;

        private float _rayDistance;
        private bool wallDetected;
        private float characterRadius;
        private float characterHeight;
        private Vector3 _middlePosition => Orientation.position + (characterHeight * 0.5f * Vector3.up);
        private Vector3 _topPosition => Orientation.position + (characterHeight * Vector3.up);
        private Vector3 _bottomPosition => Orientation.position;

        private void Awake()
        {
            var capsule = GetComponent<CharacterController>();
            characterHeight = capsule.height;
            characterRadius = capsule.radius;
            _rayDistance = characterRadius + RayDistance;
        }

        public bool IsWallDetected(Vector3 inputDirection)
        {
            Ray topRay = new Ray(_topPosition, inputDirection);
            Ray middleRay = new Ray(_middlePosition, inputDirection);
            Ray bottomRay = new Ray(_bottomPosition, inputDirection);

            bool topDetected = Physics.Raycast(topRay, _rayDistance, layerData.WhatIsWall);
            bool middleDetected = Physics.Raycast(middleRay, _rayDistance, layerData.WhatIsWall);
            bool bottomDetected = Physics.Raycast(bottomRay, _rayDistance, layerData.WhatIsWall);

            return topDetected || middleDetected || bottomDetected;
        }

        private bool FrontWallCheck()
        {
            Ray topRay = new Ray(_topPosition, Orientation.forward);
            Ray middleRay = new Ray(_middlePosition, Orientation.forward);
            Ray bottomRay = new Ray(_bottomPosition, Orientation.forward);

            bool topDetected = Physics.Raycast(topRay,       _rayDistance, layerData.WhatIsWall);
            bool middleDetected = Physics.Raycast(middleRay, _rayDistance, layerData.WhatIsWall);
            bool bottomDetected = Physics.Raycast(bottomRay, _rayDistance, layerData.WhatIsWall);
            return topDetected || middleDetected || bottomDetected;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                if(DrawGizmosRay)
                {
                    Ray topRay = new Ray(_topPosition, Orientation.forward * _rayDistance);
                    Ray middleRay = new Ray(_middlePosition, Orientation.forward * _rayDistance);
                    Ray bottomRay = new Ray(_bottomPosition, Orientation.forward * _rayDistance);

                    Gizmos.color = RayColor;
                    Gizmos.DrawRay(_topPosition, Orientation.forward * _rayDistance);
                    Gizmos.DrawRay(_middlePosition, Orientation.forward * _rayDistance);
                    Gizmos.DrawRay(_bottomPosition, Orientation.forward * _rayDistance);

                    //Gizmos.color = Color.blue;
                    //Gizmos.DrawRay(_topPosition, Orientation.forward * 0.01f);
                    //Gizmos.DrawRay(_middlePosition, Orientation.forward * 0.01f);
                    //Gizmos.DrawRay(_bottomPosition, Orientation.forward * 0.01f);
                }
            }
        }
        private bool SideWallCheck()
        {
            // TODO
            return true;
        }
    }
}

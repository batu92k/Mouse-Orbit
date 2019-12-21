﻿/**
  ******************************************************************************
  * @file    Orbiter.cs
  * @author  Ali Batuhan KINDAN
  * @date    20.12.2019
  * @brief   This file contains the classes amd methods for handling the Mouse
  *          Orbit, Mouse Pan and Mouse Zoom calculations
  ******************************************************************************
  */

/*
 *  Mouse Orbit - Quaternion Based Mouse Orbit Algorithm
 *  Copyright (C) 2017, 2019  Ali Batuhan KINDAN
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System;

namespace Mouse_Orbit
{

    public class Orbiter
    {
        const double deg2Rad = Math.PI / 180.0;
        const double rad2Deg = 180.0 / Math.PI;
        int mouseX_Old = 0;
        int mouseY_Old = 0;
        int difX = 0;
        int difY = 0;
        Quaternion qdMouse = new Quaternion(1, 0, 0, 0);
        Quaternion qGlobal_E = new Quaternion(1, 0, 0, 0);
        Quaternion qGlobal = new Quaternion(1, 0, 0, 0);
        Vector3 vdMouse = new Vector3(0, 0, 0);

        public struct Orbit
        {
            public float angle, ox, oy, oz;
            public Orbit(float angleDeg = 0, float x = 0, float y = 0, float z = 0)
            {
                angle = angleDeg;
                ox = x;
                oy = y;
                oz = z;
            }
        }

        private struct Quaternion
        {
            public float qw, qx, qy, qz;
            public Quaternion(float w = 1, float x = 0, float y = 0, float z = 0)
            {
                qw = w;
                qx = x;
                qy = y;
                qz = z;
            }
        }

        private struct Vector3
        {
            public float vx, vy, vz;
            public Vector3(float x = 1, float y = 1, float z = 1)
            {
                vx = x;
                vy = y;
                vz = z;
            }
        }

        /**
          * @brief  This function is using to make a rotation quaternion with
          *         given vector and rotation amount in radians
          * @param  radTheta
          * @param  v
          * @retval Quaternion
          */
        private Quaternion Quat_Make(float radTheta, Vector3 v)
        {
            Quaternion q = new Quaternion
            {
                qw = (float)Math.Cos(radTheta / 2.0f),
                qx = v.vx * (float)Math.Sin(radTheta / 2.0f),
                qy = v.vy * (float)Math.Sin(radTheta / 2.0f),
                qz = v.vz * (float)Math.Sin(radTheta / 2.0f)
            };

            return q;
        }

        /**
          * @brief  This function is using to normalize quaternions
          *         We need to normaize the quaternions before the multiplication
          *         they must be unit quaternions
          * @param  qN
          * @retval none
          */
        private void Quat_Normalize(ref Quaternion qN)
        {
            /* add very small value to avoid divide by zero error */
            float R = (float)Math.Sqrt(qN.qw * qN.qw + qN.qx * qN.qx + qN.qy * qN.qy + qN.qz * qN.qz) + 0.0000001f;
            qN.qw /= R;
            qN.qx /= R;
            qN.qy /= R;
            qN.qz /= R;
        }

        /**
          * @brief  This function is using to multiply quaternions (Hamilton Product)
          * @param  q1
          * @param  q2
          * @retval Quaternion
          */
        private Quaternion Quat_Multiply(Quaternion q1, Quaternion q2)
        {
            Quaternion q3 = new Quaternion
            {
                /* Hamilton Product Assemble Routine */
                qw = (q1.qw * q2.qw - q1.qx * q2.qx - q1.qy * q2.qy - q1.qz * q2.qz),
                qx = (q1.qw * q2.qx + q1.qx * q2.qw + q1.qy * q2.qz - q1.qz * q2.qy),
                qy = (q1.qw * q2.qy - q1.qx * q2.qz + q1.qy * q2.qw + q1.qz * q2.qx),
                qz = (q1.qw * q2.qz + q1.qx * q2.qy - q1.qy * q2.qx + q1.qz * q2.qw)
            };

            return q3;
        }

        /**
          * @brief  This function is using for calculate the derivative of mouse coordinates.
          *         Calculated derivative values are using in the orbit and pan features.
          *         This function needs to be called periodically inside a timer or thread 
          *         to make orbit and pan features work.
          * @param  mouseX
          * @param  mouseY
          * @retval none
          */
        public void Update_Coordinates(int mouseX, int mouseY)
        {
            difX = mouseX - mouseX_Old;
            difY = -(mouseY - mouseY_Old);
            mouseX_Old = mouseX;
            mouseY_Old = mouseY;
        }

        /**
          * @brief  This function is using to calculate current raotation quaternion from
          *         mouse coordinates that are updated from Update_Coordinates function.
          * @param  mouseX
          * @param  mouseY
          * @retval Quaternion
          */
        public Orbit Get_Orbit()
        {
            if(difX != 0 || difY != 0)
            {
                /* rotate mouse derivative vector 90 degrees and assign to mouse vector 
                   in order to assemble the rotation shaft. to rotate 90 degrees CCW we
                   need to multiply coordinate values with "i". if v = x + yi , v' = i * v 
                   so v' = -y + xi which is the rotated vector.*/
                vdMouse.vx = -difY;
                vdMouse.vy = difX;
                vdMouse.vz = 0;

                float dTheta = -(float)Math.Sqrt(vdMouse.vx * vdMouse.vx + vdMouse.vy * vdMouse.vy);
                qdMouse = Quat_Make((float)(dTheta * deg2Rad), vdMouse);
                Quat_Normalize(ref qdMouse); /* normalize before rotation */
                qGlobal = Quat_Multiply(qdMouse, qGlobal_E); /* inverse rotation */
                Quat_Normalize(ref qGlobal); /* normalize after rotation */
                qGlobal_E = qGlobal; /* update old value to use it in the next iteration */
            }

            return new Orbit((float)(2 * Math.Acos(qGlobal.qw) * rad2Deg), qGlobal.qx, qGlobal.qy, qGlobal.qz);
        }

    }
}

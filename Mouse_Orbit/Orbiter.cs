/**
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

    }
}

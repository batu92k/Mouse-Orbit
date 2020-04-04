/**
  ******************************************************************************
  * @file    BatuGL.cs
  * @author  Ali Batuhan KINDAN
  * @date    19.12.2019
  * @brief   This file contains the methods for opengl graphics features
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

using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Mouse_Orbit
{
    public static class BatuGL
    {
        /**
          * @brief  This function is using to make initial configuration to
          *         GL Control element before drawing
          * @param  refGLControl
          * @retval none
          */
        public static void Configure(OpenTK.GLControl refGLControl)
        {
            GL.ClearColor(Color.Black);
            refGLControl.VSync = false;
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color.Gray);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Viewport(refGLControl.Size);
            GL.Ortho(-refGLControl.Width / 2, refGLControl.Width / 2, -refGLControl.Height / 2, refGLControl.Height / 2, -20000, 20000);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearDepth(1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        /**
          * @brief  This function draws a cube around the origin with given size
          * @param  size
          * @retval none
          */
        public static void DrawCube(int size)
        {
            /* draw unit cube then scale it with given size */
            GL.PushMatrix();
            GL.Scale(size, size, size);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Crimson);
            GL.Vertex3(0.5f, 0.5f, -0.5f);
            GL.Vertex3(-0.5f, 0.5f, -0.5f);
            GL.Vertex3(-0.5f, -0.5f, -0.5f);
            GL.Vertex3(0.5f, -0.5f, -0.5f);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, -0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, 0.5f);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.DarkOrange);
            GL.Vertex3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, -0.5f);
            GL.Vertex3(0.5f, 0.5f, -0.5f);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.BlueViolet);
            GL.Vertex3(-0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, -0.5f, 0.5f);
            GL.Vertex3(-0.5f, -0.5f, -0.5f);
            GL.Vertex3(-0.5f, 0.5f, -0.5f);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Purple);
            GL.Vertex3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, 0.5f, -0.5f);
            GL.Vertex3(0.5f, 0.5f, -0.5f);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.LightSeaGreen);
            GL.Vertex3(0.5f, -0.5f, 0.5f);
            GL.Vertex3(-0.5f, -0.5f, 0.5f);
            GL.Vertex3(-0.5f, -0.5f, -0.5f);
            GL.Vertex3(0.5f, -0.5f, -0.5f);
            GL.End();

            GL.PopMatrix();
        }


    }
}

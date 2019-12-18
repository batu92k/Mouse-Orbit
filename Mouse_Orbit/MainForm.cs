/**
  ******************************************************************************
  * @file    MainForm.cs
  * @author  Ali Batuhan KINDAN
  * @date    17.12.2019
  * @brief   This program performs the quaternion based mouse orbit algorithm
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace Mouse_Orbit
{
    public partial class MainForm : Form
    {
        bool monitorLoaded = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void GL_Monitor_Load(object sender, EventArgs e)
        {
            monitorLoaded = true;
            GL.ClearColor(Color.Black);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL_Monitor.VSync = false;
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color.Black);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Viewport(GL_Monitor.Size);
            GL.Ortho(-GL_Monitor.Width / 2, GL_Monitor.Width / 2, -GL_Monitor.Height / 2, GL_Monitor.Height / 2, 20000, -20000);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearDepth(1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
        }

        private void GL_Monitor_Paint(object sender, PaintEventArgs e)
        {
            if (!monitorLoaded)
                return;

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.ClearColor(Color.Black);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Viewport(GL_Monitor.Size);
            GL.Ortho(-GL_Monitor.Width / 2, GL_Monitor.Width / 2, -GL_Monitor.Height / 2, GL_Monitor.Height / 2, 20000, -20000);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearDepth(1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL_Monitor.SwapBuffers();
        }
    }
}

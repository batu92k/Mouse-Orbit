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
        int i = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void GL_Monitor_Load(object sender, EventArgs e)
        {
            monitorLoaded = true;
            GL.ClearColor(Color.Black);
            DrawTimer.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BatuGL.Configure(GL_Monitor);
        }

        private void GL_Monitor_Paint(object sender, PaintEventArgs e)
        {
            if (!monitorLoaded)
                return;

            BatuGL.Configure(GL_Monitor);
            /* lets rotate the cube for now */
            GL.Rotate(i, 1, 0, 0);
            GL.Rotate(i / 2, 0, 0, 1);
            i += 5;
            i %= 360;
            BatuGL.DrawCube(200);

            GL_Monitor.SwapBuffers();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            GL_Monitor.Invalidate();
        }
    }
}

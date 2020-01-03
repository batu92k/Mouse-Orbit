/**
  ******************************************************************************
  * @file    MainForm.cs
  * @author  Ali Batuhan KINDAN
  * @date    17.12.2019
  * @brief   This program performs the quaternion based mouse orbit algorithm. The
  *          app is the re-make of my older mouse orbit implementation that I made 
  *          in 2017 with an older OpenGL API (Tao Framework).
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
        Orbiter orb;

        public MainForm()
        {
            InitializeComponent();
            orb = new Orbiter();
            GL_Monitor.MouseDown += orb.Control_MouseDownEvent;
            GL_Monitor.MouseUp += orb.Control_MouseUpEvent;
            GL_Monitor.MouseWheel += orb.Control_MouseWheelEvent;
            GL_Monitor.KeyPress += orb.Control_KeyPress_Event;
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
            GL.Translate(orb.PanX, orb.PanY, 0);
            GL.Scale(orb.scaleVal, orb.scaleVal, orb.scaleVal);
            GL.Rotate(orb.orbitStr.angle, orb.orbitStr.ox, orb.orbitStr.oy, orb.orbitStr.oz);
            BatuGL.DrawCube(200);
            GL_Monitor.SwapBuffers();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            orb.UpdateOrbiter(MousePosition.X, MousePosition.Y);
            GL_Monitor.Invalidate();
        }
    }
}

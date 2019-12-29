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
        Orbiter orb = new Orbiter();
        Orbiter.Orbit orbitStr;
        bool enableOrbit = false;
        bool enablePan = false;
        float scaleVal = 1.0f; /* initial scale value for the opengl drawing */
        int mouseX_Old = 0;
        int mouseY_Old = 0;
        int difX = 0;
        int difY = 0;
        int PanX = 0;
        int PanY = 0;

        public MainForm()
        {
            InitializeComponent();
            GL_Monitor.MouseWheel += GL_Monitor_MouseWheel;
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
            GL.Translate(PanX, PanY, 0);
            GL.Scale(scaleVal, scaleVal, scaleVal);
            GL.Rotate(orbitStr.angle, orbitStr.ox, orbitStr.oy, orbitStr.oz);
            BatuGL.DrawCube(200);
            GL_Monitor.SwapBuffers();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            difX = MousePosition.X - mouseX_Old;
            difY = -(MousePosition.Y - mouseY_Old); /* set origin point to bottom left from top left */
            mouseX_Old = MousePosition.X;
            mouseY_Old = MousePosition.Y;
            if (enableOrbit)
            {
                orbitStr = orb.Get_Orbit(difX, difY);
            }
            else if (enablePan)
            {
                PanX += difX;
                PanY += difY;
            }
            GL_Monitor.Invalidate();
        }

        private void GL_Monitor_MouseDown(object sender, MouseEventArgs e)
        {
            enableOrbit = (e.Button == MouseButtons.Right);
            enablePan = (e.Button == MouseButtons.Left);
        }

        private void GL_Monitor_MouseUp(object sender, MouseEventArgs e)
        {
            enableOrbit = (e.Button == MouseButtons.Right) ? false : enableOrbit;
            enablePan = (e.Button == MouseButtons.Left) ? false : enablePan;
        }

        private void GL_Monitor_MouseWheel(object sender, MouseEventArgs e)
        {
            scaleVal += (e.Delta > 0) ? 0.1f : -0.1f;
            if (scaleVal < 0.1f) scaleVal = 0.1f;
            else if (scaleVal > 5.0f) scaleVal = 5.0f;
        }

        private void GL_Monitor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'r')
            {
                /* reset Orbit Pan and Scale to default values */
                orb.Reset_Orientation();
                orbitStr = orb.Get_Orbit(0, 0);
                PanX = 0;
                PanY = 0;
                scaleVal = 1.0f;
            }
        }
    }
}

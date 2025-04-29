using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tpc_chuva
{
    class ClsCirculo
    {
        VertexPosition[] vertices;
        BasicEffect effect;
        public Matrix worldMatrix;
        VertexBuffer vertexBuffer;
        public Vector3 position;

        public ClsCirculo(GraphicsDevice device, int nSides, float r, float h, Vector3 pos)
        {
            //effects do circulo 
            effect = new BasicEffect(device);
            position = pos;
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(10.0f, 7f, 5.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 0.1f, 1000.0f);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = true;
            worldMatrix = Matrix.Identity;
            CreateGeometry(device, nSides, r, h);
        }
        private void CreateGeometry(GraphicsDevice device, int nSides, float r, float h) // funçao responsavel por gerar a geometria 
        {
            int vertexCount = (nSides) * 6;
            vertices = new VertexPosition[vertexCount];

            //vertices que formam o circulo e o seu repetivo vertexBuffer
            for (int i = 0; i <= nSides; i++)
            {
                float angulo = MathHelper.ToRadians(i * (360.0f / nSides));

                float x = r * (float)System.Math.Cos((double)angulo);
                float z = -r * (float)System.Math.Sin((double)angulo);

                float u = 0.5f + 0.5f * (float)System.Math.Cos((double)angulo);
                float v = 0.5f - 0.5f * (float)System.Math.Sin((double)angulo);
                float texX = 1.0f / nSides;

                vertices[2 * i + 0] = new VertexPosition(new Vector3(x, h, z));
                vertices[2 * i + 1] = new VertexPosition(new Vector3(0, h, 0));
            }
                vertexBuffer = new VertexBuffer(device, typeof(VertexPosition), vertexCount, BufferUsage.None); // Cria o vertexBuffer no qual só desenha apenas 1 vez 
                vertexBuffer.SetData<VertexPosition>(vertices);
        }
        public void Draw(GraphicsDevice device)
        {

            // Indica o efeito para desenhar os eixos
            effect.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(vertexBuffer);
            device.DrawUserPrimitives<VertexPosition>(PrimitiveType.TriangleStrip, vertices, 0, vertices.Length - 2);
        }
    }
}

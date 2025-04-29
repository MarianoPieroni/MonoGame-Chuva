using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tpc_chuva
{
    class ClsPlano
    {
        VertexPositionColorTexture[] vertices;
        BasicEffect effect;
        Matrix worldMatrix;
        VertexBuffer vertexBuffer;
        int vertexCount;
        public ClsPlano(GraphicsDevice device, Texture2D texture)
        {
            //effects do plano
            effect = new BasicEffect(device);
            float aspectRatio = (float)device.Viewport.Width /
            device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(10.0f, 7f, 5.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 0.1f, 1000.0f);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = true;
            effect.Texture = texture;
            effect.TextureEnabled = true;
            CreateGeometry(device);
            worldMatrix = Matrix.Identity;
        }
        private void CreateGeometry(GraphicsDevice device) //funçao responsavel por gerar a geometria do plano
        {
            float axisLenght = 5f; // Tamanho da linha a cada vertice do plano
                                   //numero de vertices(vertexCount) e o seu respetivo array de vertices(vertices)
            vertexCount = 4;
            vertices = new VertexPositionColorTexture[vertexCount];
            //vertices que formam o plano e o seu repetivo vertexBuffer
            vertices[0] = new VertexPositionColorTexture(new Vector3(-axisLenght, 0.0f, -axisLenght), Color.White, new Vector2(0.0f, 0.0f));
            vertices[1] = new VertexPositionColorTexture(new Vector3(+axisLenght, 0.0f, -axisLenght), Color.White, new Vector2(1.0f, 0.0f));
            vertices[2] = new VertexPositionColorTexture(new Vector3(-axisLenght, 0.0f, +axisLenght), Color.White, new Vector2(0.0f, 1.0f));
            vertices[3] = new VertexPositionColorTexture(new Vector3(+axisLenght, 0.0f, +axisLenght), Color.White, new Vector2(1.0f, 1.0f));
            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionColorTexture), vertexCount, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColorTexture>(vertices);
        }
        public void Draw(GraphicsDevice device)
        {

            // World Matrix
            effect.World = worldMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            // Indica o efeito para desenhar os eixos
            device.SetVertexBuffer(vertexBuffer);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, vertices.Length - 2);


        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tpc_chuva
{
    class ClsSystemParticula
    {
        VertexPositionColor[] vertice_chuva;
        List<ClsParticula> particulas;
        System.Random r;
        int particulas_por_segundo;
        GraphicsDevice device;
        BasicEffect effect;
        Matrix worldMatrix;
        Vector3 position;
        ClsCirculo circulo;

        public ClsSystemParticula(GraphicsDevice device, ClsCirculo circulo)
        {
            //effects da chuva 
            effect = new BasicEffect(device);
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(10.0f, 7f, 5.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 0.1f, 1000.0f);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = true;
            worldMatrix = Matrix.Identity;

            //armazena as variaveis passadas por parametro
            this.circulo = circulo;
            this.position = circulo.position;
            this.device = device;

            //variavel responsavel pelo randon
            r = new System.Random(1);

            particulas_por_segundo = 700;
            particulas = new List<ClsParticula>();
        }
        public void Update(GameTime gameTime)
        {
            //gera as particulas
            int gerar_particulas = (int)Math.Round(this.particulas_por_segundo * (float)gameTime.ElapsedGameTime.TotalSeconds);
            for (int i = 0; i < gerar_particulas; i++)
            {
                particulas.Add(this.Gerador());
            }

            //mata as particulas 
            List<Vector3> fs = new List<Vector3>();
            List<Vector3> accs = new List<Vector3>();
            accs.Add(new Vector3(0, -9.8f, 0));
            for (int i = particulas.Count - 1; i > 0; i--)
            {
                if ((particulas[i].chuvaParticula[1].Position.Y - particulas[i].chuvaParticula[0].Position.Y) / 2f
                     + particulas[i].postion.Y < 0)
                {
                    particulas.RemoveAt(i);
                }
                else particulas[i].Update(gameTime, fs, accs);
            }
           

        }
        public void Draw(GraphicsDevice device)
        {

            // cria e desenha as gotas da chuva
            effect.World = worldMatrix;
            vertice_chuva = new VertexPositionColor[particulas.Count * 2];
            for (int i = 0; i < particulas.Count; i++)
            {
                vertice_chuva[2 * i + 0] = new VertexPositionColor(particulas[i].postion, Color.Blue);
                Vector3 vel_normal = particulas[i].velocidade;
                vel_normal.Normalize();
                vel_normal = vel_normal * 0.1f; //tamanho da particula
                vertice_chuva[2 * i + 1] = new VertexPositionColor(particulas[i].postion - vel_normal, Color.Blue);
            }
            // Indica o efeito para desenhar os eixos
            effect.CurrentTechnique.Passes[0].Apply();
            device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertice_chuva, 0, particulas.Count);

        }
        ClsParticula Gerador() //funçao responsavel por gerar as particulas 
        {
            Vector3 pos;
            Vector3 vel;
            float massa;
            massa = 1;
            float x, y, z;
            x = -0.1f + (float)(r.NextDouble()) * 0.2f;
            z = -0.1f + (float)(r.NextDouble()) * 0.2f;
            y = -0.1f + (float)(r.NextDouble()) * 0.2f;

            //calcula o local a onde vai ser gerado 
            float distanciaAoCentro = (float)r.NextDouble() * (5) * r.Next(-1, 2);
            Vector3 randomDirection = new Vector3(
                MathF.Cos(MathHelper.ToRadians((float)r.NextDouble() * r.Next(0, 361))),
                0,
                MathF.Sin(MathHelper.ToRadians((float)r.NextDouble() * r.Next(0, 361))));

            //calcula a velocidade
            vel = new Vector3(x, y, z);
            pos = circulo.position + randomDirection * distanciaAoCentro;

            ClsParticula nova_particula = new ClsParticula(this.device, pos, vel);
            return nova_particula;
        }
    }
}

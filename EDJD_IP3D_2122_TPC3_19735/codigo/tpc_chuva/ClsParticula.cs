using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tpc_chuva
{
    class ClsParticula
    {
        public Vector3 postion;
        public Vector3 velocidade;
        public float massa;
        BasicEffect effect;
        Matrix worldMatrix;
        public VertexPositionColor[] chuvaParticula = new VertexPositionColor[2];

        public ClsParticula(GraphicsDevice device,Vector3 pos, Vector3 vel)
        {
            //armazena as variaveis passadas por parametro
            this.postion = pos;
            this.velocidade = vel;
            massa = 1;
        }
        public void Update(GameTime gameTime, List<Vector3> força, List<Vector3> aceleraçoes)
        {
            // calculo das forças para uzar na aceleraçao
            Vector3 forçaTotal = Vector3.Zero;
            foreach (Vector3 frç in força)
            {
                forçaTotal += frç;
            }
            Vector3 aceleraçao = Vector3.Zero;
            aceleraçao = forçaTotal / massa;

            foreach (Vector3 ac in aceleraçoes)
            {
                aceleraçao += ac;
            }
            // calculo da velocidade e posiçao 
            velocidade = velocidade + aceleraçao * (float)gameTime.ElapsedGameTime.TotalSeconds;
            postion = postion + velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
            worldMatrix = Matrix.CreateTranslation(this.postion);
        }
    }
}

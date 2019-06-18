using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    static class TextureManager
    {
        public static Dictionary<string, Texture> dictionary;

        static TextureManager()
        {
            dictionary = new Dictionary<string, Texture>();
            dictionary.Add("Bottom tube", new Texture("Assets/Bottom_Tube.png"));
            dictionary.Add("Bird", new Texture("Assets/Bird.png"));
            dictionary.Add("Exit", new Texture("Assets/exit.png"));
            dictionary.Add("Number", new Texture("Assets/number.png"));
            dictionary.Add("Background", new Texture("Assets/sfondo.jpg"));
            dictionary.Add("Start", new Texture("Assets/start.png"));
            dictionary.Add("Terrain", new Texture("Assets/terrain.jpg"));
            dictionary.Add("Top tube", new Texture("Assets/Top_Tube.png"));
        }

        //RETURN A TEXTURE
        //Return an texture from texture dictionary
        public static Texture GetTexture(string TextureName)
        {
            return dictionary[TextureName];
        }
    }
}

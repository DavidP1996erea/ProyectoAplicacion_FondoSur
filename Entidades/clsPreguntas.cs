using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsPreguntas
    {

        #region Atributos
        public string category { get; set; }
        public string id { get; set; }
        public string correctAnswer { get; set; }
        public List <string> incorrectAnswers { get; set; }

        public string question { get; set; }
        public List<string> tags { get; set; }
        public string type { get; set; }
        public string difficulty { get; set; }
        public List <string> regions { get; set; }
        public bool isNiche { get; set; }

        #endregion

        #region Constructores

        public clsPreguntas()
        {

        }
        public clsPreguntas(string category, string id, string correctAnswer, List<string> incorrectAnswers, string question, List<string> tags, string type, string difficulty, List<string> regions, bool isNiche)
        {
            this.category = category;
            this.id = id;
            this.correctAnswer = correctAnswer;
            this.incorrectAnswers = incorrectAnswers;
            this.question = question;
            this.tags = tags;
            this.type = type;
            this.difficulty = difficulty;
            this.regions = regions;
            this.isNiche = isNiche;
        }


        #endregion

    }
}

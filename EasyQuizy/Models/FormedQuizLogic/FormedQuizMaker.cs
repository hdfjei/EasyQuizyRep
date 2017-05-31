using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EasyQuizy.Models.FormedQuizLogic
{
    public enum GenerationType { One, Two }
    public class FormedQuizMaker
    {
        QuizContext db = new QuizContext();
        int variantsNumber;
        int questionsNumber;

        int[] generalQuizes;
        bool[] isChoosen;

        public FormedQuizMaker(int[] generalQuizes, bool[] isChoosen, int variantsNumber, int questionNumber)
        {
            this.generalQuizes = generalQuizes;
            this.isChoosen = GetIsChoosenArrayWithoutDublicate(isChoosen);
            this.variantsNumber = variantsNumber;
            this.questionsNumber = questionNumber;
        }
        public bool[] GetIsChoosenArray()
        {
            return isChoosen;
        }
        private bool[] GetIsChoosenArrayWithoutDublicate(bool[] isChoosen)
        {
            List<bool> temp = new List<bool>();
            for (int i = 0; i < isChoosen.Length; i++)
            {
                if (!isChoosen[i])
                {
                    temp.Add(isChoosen[i]);
                }
                else
                {
                    temp.Add(isChoosen[i]);
                    i++;
                }
            }
            return temp.ToArray();
        }
        public int[] FindChoosenQuizes()
        {
            List<int> temp = new List<int>(); 
            for (int i = 0; i < isChoosen.Length; i++)
            {
                if (isChoosen[i])
                {
                    temp.Add(generalQuizes[i]);
                }
            }
            return temp.ToArray();
        }
        private List<Question> GetQuestionsList(int[] choosenQuizes)
        {
            List<Question> result = new List<Question>();
            foreach (var item in choosenQuizes)
            {
                result.AddRange(db.Questions.Where(q => q.GeneralQuizId == item).Include(q=>q.Answers));
            }
            db.Dispose();
            return result;
        }
        public List<Question>[] FormQuizes(GenerationType type)
        {
            int[] choosenQuizes = FindChoosenQuizes();
            if (choosenQuizes != null)
            {
                List<Question> allQuestions = GetQuestionsList(choosenQuizes);

                List<Question>[] questionsByVariants = new List<Question>[variantsNumber];
                questionsByVariants = QuestionByVariantsInitizlizer(questionsByVariants);

                switch (type)
                {
                    case GenerationType.One:
                        GenerateQuizByTypeOne(allQuestions, ref questionsByVariants);
                        break;
                    case GenerationType.Two:
                        GenerateQuizByTypeTwo(allQuestions, ref questionsByVariants);
                        break;
                    default:
                        break;
                }
                return questionsByVariants;
            }
            else
            {
                throw new Exception();
            }
        }
        private List<Question>[] QuestionByVariantsInitizlizer(List<Question>[] questionsByVariants)
        {
            for (int i = 0; i < questionsByVariants.Length; i++)
            {
                questionsByVariants[i] = new List<Question>();
            }
            return questionsByVariants;
        }
        private void GenerateQuizByTypeOne(List<Question> allQuestions, ref List<Question>[] questionsByVariants)
        {
            int counter = 0;
            for (int i = 0; i < questionsNumber; i++)
            {
                for (int j = 0; j < variantsNumber; j++)
                {
                    if (counter == allQuestions.Count)
                    {
                        counter = 1;
                    }
                    questionsByVariants[j].Add(allQuestions[counter]);
                    counter++;
                }
            }
        }
        private void GenerateQuizByTypeTwo(List<Question> allQuestions, ref List<Question>[] questionsByVariants)
        {
            int selection = allQuestions.Count / variantsNumber;

            int currentPosition = 0;
            int currentStep = 0;
            int currentVariant = 0;
            int initialVariant = 0;
            for (int i = 0; i < questionsNumber; i++)
            {
                currentVariant = initialVariant;
                currentStep = 0;
                for (int j = 0; j < variantsNumber; j++)
                {
                    questionsByVariants[currentVariant].Add(allQuestions[currentPosition + currentStep]);
                    currentStep += selection;
                    currentVariant++;
                    if (currentVariant == variantsNumber)
                    {
                        currentVariant = 0;
                    }
                    if (currentPosition == selection)
                    {
                        currentPosition = 1;
                    }
                }
                initialVariant++;
                if (initialVariant == variantsNumber)
                {
                    initialVariant = 0;
                }               
                currentPosition++;
            }
        }
    }
}
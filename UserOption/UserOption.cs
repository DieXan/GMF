namespace UserOption
{
    public class UO
    {
        private Dictionary<int, int> step = new Dictionary<int, int>() ;
        //public Dictionary<int, bool> DrinkOption = new Dictionary<int, bool>();
        public Dictionary<int, int> Step
        {
            get { return step; }
            set { step = value; }
        }
        public void ChangeStep(int user_id, int step)
        {
            Dictionary<int, int> _Step = new Dictionary<int, int>();
            _Step = Step;
            if (Step.ContainsKey(user_id) == true)
            {
                _Step[user_id] = step;
                Step = _Step;
            }
            else
            {
                _Step.Add(user_id, step);
                Step = _Step;
            }
        }
        
        public UO()
        {
            Dictionary<int, int> _Step = new Dictionary<int, int>();
            //Dictionary<int, bool> _DrinkOption = new Dictionary<int, bool>();
            Step = _Step;
            //DrinkOption = _DrinkOption;
        }
        /*public void ChangeDrinkOption(int user_id, bool NeedOrNot)
        {
            if (DrinkOption.ContainsKey(user_id) == true)
            {
                DrinkOption[user_id] = NeedOrNot;
            }
            else
            {
                DrinkOption.Add(user_id, NeedOrNot);
            }
        }*/


    }
}
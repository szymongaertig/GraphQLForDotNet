namespace CatteryRegister.Model
{
    public class Parents
    {
        public Parents(int childId, int femaleParentId, int maleParentId)
        {
            ChildId = childId;
            FemaleParentId = femaleParentId;
            MaleParentId = maleParentId;
        }

        public int MaleParentId { get; set; }
        public int FemaleParentId { get; set; }
        public int ChildId { get; set; }
    }
}
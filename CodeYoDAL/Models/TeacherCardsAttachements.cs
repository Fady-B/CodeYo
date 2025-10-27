using CodeYoDAL.Models;

namespace ShaghalnyDAL.Models
{
    public class TeacherCardsAttachements
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        //public string FrontCardName
        //{
        //    get { return Id.ToString() + "_FrontCard"; }
        //}
        public string FrontCardName { get; set; }
        public string BackCardName { get; set; }
        public float CardWidth { get; set; }
        public float CardHeight { get; set; }
        public bool IsQRInFrontCard { get; set; } = true;
        public float QRFrontTopPixels { get; set; }
        public float QRFrontLeftPixels { get; set; }
        public bool IsQRInBackCard { get; set; }
        public float QRBackTopPixels { get; set; }
        public float QRBackLeftPixels { get; set; }

        public Teachers Teacher { get; set; }
    }
}

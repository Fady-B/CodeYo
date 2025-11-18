using CodeYoDAL.Models;
using Microsoft.AspNetCore.Http;

namespace CodeYoDAL.ViewModels
{
    public class CardsDesignViewModel : EntityBase
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public IFormFile FrontCardFile { get; set; }
        public IFormFile BackCardFile { get; set; }
        public string FrontCardName { get; set; }
        public string BackCardName { get; set; }
        public float CardWidth { get; set; }
        public float CardHeight { get; set; }

        //Front Card
        public bool IsQRInFrontCard { get; set; } = true;
        //public IFormFile QRInFrontCardFile { get; set; }
        public float QRFrontSizePercent { get; set; }
        public float QRFrontTopPixels { get; set; }
        public float QRFrontLeftPixels { get; set; }


        //Back Card
        public bool IsQRInBackCard { get; set; }
        //public IFormFile QRInBackCardFile { get; set; }
        public float QRBackSizePixels { get; set; }
        public float QRBackTopPixels { get; set; }
        public float QRBackLeftPixels { get; set; }

        public string FrontHtmlDataContent { get; set; }
        public string BackHtmlDataContent { get; set; }


        public List<StudentsViewModel> TeacherStudents { get; set; } = new List<StudentsViewModel>();

        public static implicit operator CardsDesignViewModel(TeacherCardsAttachements _TeacherCardsAttachements)
        {
            return new CardsDesignViewModel
            {
                Id = _TeacherCardsAttachements.Id,
                TeacherId = _TeacherCardsAttachements.TeacherId,
                FrontCardName = _TeacherCardsAttachements.FrontCardName,
                BackCardName = _TeacherCardsAttachements.BackCardName,
                CardWidth = _TeacherCardsAttachements.CardWidth,
                CardHeight = _TeacherCardsAttachements.CardHeight,
                IsQRInFrontCard = _TeacherCardsAttachements.IsQRInFrontCard,
                QRFrontSizePercent = _TeacherCardsAttachements.QRFrontSizePercent,
                QRFrontTopPixels = _TeacherCardsAttachements.QRFrontTopPixels,
                QRFrontLeftPixels = _TeacherCardsAttachements.QRFrontLeftPixels,
                IsQRInBackCard = _TeacherCardsAttachements.IsQRInBackCard,
                QRBackSizePixels = _TeacherCardsAttachements.QRBackSizePixels,
                QRBackTopPixels = _TeacherCardsAttachements.QRBackTopPixels,
                QRBackLeftPixels = _TeacherCardsAttachements.QRBackLeftPixels,
                FrontHtmlDataContent = _TeacherCardsAttachements.FrontHtmlDataContent,
                BackHtmlDataContent = _TeacherCardsAttachements.BackHtmlDataContent,

                CreatedDate = _TeacherCardsAttachements.CreatedDate,
                ModifiedDate = _TeacherCardsAttachements.ModifiedDate,
                CreatedBy = _TeacherCardsAttachements.CreatedBy,
                ModifiedBy = _TeacherCardsAttachements.ModifiedBy,
                Cancelled = _TeacherCardsAttachements.Cancelled,
            };
        }

        public static implicit operator TeacherCardsAttachements(CardsDesignViewModel vm)
        {
            return new TeacherCardsAttachements
            {
                Id = vm.Id,
                TeacherId = vm.TeacherId,
                FrontCardName = vm.FrontCardName,
                BackCardName = vm.BackCardName,
                CardWidth = vm.CardWidth,
                CardHeight = vm.CardHeight,
                IsQRInFrontCard = vm.IsQRInFrontCard,
                QRFrontSizePercent = vm.QRFrontSizePercent,
                QRFrontTopPixels = vm.QRFrontTopPixels,
                QRFrontLeftPixels = vm.QRFrontLeftPixels,
                IsQRInBackCard = vm.IsQRInBackCard,
                QRBackSizePixels = vm.QRBackSizePixels,
                QRBackTopPixels = vm.QRBackTopPixels,
                QRBackLeftPixels = vm.QRBackLeftPixels,
                FrontHtmlDataContent = vm.FrontHtmlDataContent,
                BackHtmlDataContent = vm.BackHtmlDataContent,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}

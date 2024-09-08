namespace ClinicService.Models
{
    public class Consultation
    {
        public int ConsutationId {get; set;}
        public int PetId {get; set;}

        public int ClientId {get; set;}
        public string? Description {get; set;}
        public DateTime ConsultationDate {get; set;}
    }
}
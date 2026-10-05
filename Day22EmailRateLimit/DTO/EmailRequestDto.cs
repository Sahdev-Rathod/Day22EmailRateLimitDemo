namespace Day22EmailRateLimit.DTO
{
    public class EmailRequestDto
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IFormFile Attachment { get; set; }
    }
}
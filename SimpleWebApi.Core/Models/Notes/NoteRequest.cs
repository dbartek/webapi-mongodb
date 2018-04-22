using FluentValidation;

namespace SimpleWebApi.Core.Models.Notes
{
    public class NoteRequest
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }

    public class NoteModelValidator : AbstractValidator<NoteRequest>
    {
        public NoteModelValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Text);
        }
    }
}
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class StoryQuestionResponderInfo
    {
        [DataMember(Name = "question_id")]
        public string QuestionId { get; set; }

        [DataMember(Name = "question")]
        public string Question { get; set; }

        [DataMember(Name = "question_type")]
        public string QuestionType { get; set; }

        [DataMember(Name = "background_color")]
        public string BackgroundColor { get; set; }

        [DataMember(Name = "text_color")]
        public string TextColor { get; set; }

        [DataMember(Name = "responders")]
        public Responder[] Responders { get; set; }

        [DataMember(Name = "max_id")]
        public dynamic MaxId { get; set; }

        [DataMember(Name = "more_available")]
        public bool? MoreAvailable { get; set; }

        [DataMember(Name = "question_response_count")]
        public int? QuestionResponseCount { get; set; }

        [DataMember(Name = "latest_question_response_time")]
        public int? LatestQuestionResponseTime { get; set; }
    }
}
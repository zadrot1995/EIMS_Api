using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserJournalRow
    {
        public string SubjectName { get; set; }
        public List<Mark> Marks { get; set; }
        public List<Mark> PracticMarks
        {
            get
            {
                return Marks.Where(x => x.MarkType == Enums.MarkType.Practice).ToList();
            }
        }
        public Mark ExamMark
        {
            get
            {
                return Marks.Where(x => x.MarkType == Enums.MarkType.Exam).FirstOrDefault();
            }
        }
        public Mark FirstModularTestWorkMark
        {
            get
            {
                return Marks.Where(x => x.MarkType == Enums.MarkType.FirstModularTestWork).FirstOrDefault();
            }
        }
        public Mark SecondModularTestWorkMark
        {
            get
            {
                return Marks.Where(x => x.MarkType == Enums.MarkType.SecondModularTestWork).FirstOrDefault();
            }
        }
        public int Total
        {
            get
            {
                return Marks.Sum(x => x.Value);
            }
        }
    }
}

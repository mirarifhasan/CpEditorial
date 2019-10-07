using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class ViewEditorialModel : DbSchema
    {
        public Editorial editorial;
        public User user;
        public Tag tag;
        public Problem problem;
        public OnlineJudge OnlineJudge;
        public ArrayList tagList;
        public ViewEditorialModel(int editorialId)
        {
            tagList = new ArrayList();
            editorial = GetEditorial(editorialId);
            user = GetUser(editorial.userId);
            tag = GetTag(editorial.tagId); //todo: change the implementation later
            problem = GetProblem(editorial.problemId);
        }
    }
}

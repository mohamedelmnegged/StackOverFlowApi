using StackOverFlowApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class UnitOfWork
    {
        private readonly ApplicationDBContext context;

        private ActionManager actionManager;
        private CommentManager commentManager;
        private FollowerManager followerManager;
        private QuestionManager questionManager;
        private QuestionTagManager questionTagManager;
        private TagManager tagManager;
        private UserQuestionsManager userQuestionsManager;
        private WebsiteManager websiteManager; 
        public UnitOfWork(ApplicationDBContext context)
        {
            this.context = context;
        }

        public ActionManager ActionManager
        {
            get
            {
                if(actionManager == null)
                {
                    actionManager = new ActionManager(context);
                }
                return actionManager;
            }
        }public CommentManager CommentManager
        {
            get
            {
                if(commentManager == null)
                {
                    commentManager = new CommentManager(context);
                }
                return commentManager;
            }
        }public FollowerManager FollowerManager
        {
            get
            {
                if(followerManager == null)
                {
                    followerManager = new FollowerManager(context);
                }
                return followerManager;
            }
        }public QuestionManager QuestionManager
        {
            get
            {
                if(questionManager == null)
                {
                    questionManager = new QuestionManager(context);
                }
                return questionManager;
            }
        }public QuestionTagManager QuestionTagManager
        {
            get
            {
                if(questionTagManager == null)
                {
                    questionTagManager = new QuestionTagManager(context);
                }
                return questionTagManager;
            }
        }public TagManager TagManager
        {
            get
            {
                if(tagManager == null)
                {
                    tagManager = new TagManager(context);
                }
                return tagManager;
            }
        }public UserQuestionsManager UserQuestionsManager
        {
            get
            {
                if(userQuestionsManager == null)
                {
                    userQuestionsManager = new UserQuestionsManager(context);
                }
                return userQuestionsManager;
            }
        }public WebsiteManager WebsiteManager
        {
            get
            {
                if(websiteManager == null)
                {
                    websiteManager = new WebsiteManager(context);
                }
                return websiteManager;
            }
        }
    }
}

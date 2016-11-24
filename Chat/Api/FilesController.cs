using Chat.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Chat.Api
{
    public class FilesController : ApiController
    {
        private IMessageFileRepository _rep;

        public FilesController()
        {
            _rep = RepositoryLocator.Get<IMessageFileRepository>();
        }

        public void Get(string id)
        {
            var file = _rep.GetById(Guid.Parse(id));
            var response = HttpContext.Current.Response;
            response.ContentType = "application/octet-stream";
            response.AppendHeader("Content-Disposition", $"attachment; filename={file.Name}");
            response.BinaryWrite(file.Bytes);
            response.End();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Interfaces;
using WebLogic.Models;

namespace WebLogic.Services
{
    public class PathingService : IService
    {
        public PathingService()
        {
        }

        public string GetName()
        {
            return "PathingService";
        }

        public static string GetPathRoot(long pathRootId)
        {
            FilePathRoot filePathRoot = GeneralService.GetDbEntities().FilePathRoots.Where(p => p.ID == pathRootId).FirstOrDefault();
            return filePathRoot != null ? filePathRoot.FullPath : string.Empty;
        }

        public static string MapPath(long? pathRootId, string filePath, PathTypes type = PathTypes.WebPath)
        {
            string rootPath = string.Empty;
            if (pathRootId.HasValue && pathRootId.Value != 0)
                rootPath = GetPathRoot(pathRootId.Value);
            return CombinePaths(rootPath, filePath, type);
        }

        public static string CombinePaths(string rootPath, string filePath, PathTypes type = PathTypes.WebPath)
        {
            string fullPath = rootPath ?? string.Empty;
            char find = '\\', replaceWith = '/';
            if (type == PathTypes.DiskPath)
            {
                find = '/';
                replaceWith = '\\';
            }
            fullPath = fullPath.Replace(find, replaceWith);
            
            if (!string.IsNullOrEmpty(fullPath) && !fullPath.EndsWith(replaceWith.ToString()))
                fullPath += replaceWith;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                filePath = filePath.Replace(find, replaceWith);
                if (filePath.StartsWith(replaceWith.ToString()))
                    filePath = filePath.Substring(1, filePath.Length - 1);
                fullPath += filePath;
            }
            return fullPath;
        }
    }
    public enum PathTypes
    {
        WebPath = '/',
        DiskPath = '\\'
    }
}

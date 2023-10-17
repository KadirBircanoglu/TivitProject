using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_BL.InterfaceofManagers;
using TivitProject_DL.InterfaceofRepos;
using TivitProject_EL.Entities;
using TivitProject_EL.ViewModels;

namespace TivitProject_BL.ImplementationofManagers
{
    public class TivitPhotoManager : Manager<TivitPhotoDTO, TivitPhoto, int>, ITivitPhotoManager
    {
        public TivitPhotoManager(ITivitPhotoRepo repo, IMapper mapper) : base(repo, mapper, new string[] { "UserTivit" })
        {
        }
    }
}

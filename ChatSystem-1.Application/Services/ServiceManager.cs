using ChatSystem_1.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ChatSystem_1.Application.Services.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using ChatSystem_1.Domain.Entities.Models;
using ChatSystem_1.Domain.Entities.ConfigurationsModels;
using CloudinaryDotNet;


namespace ChatSystem_1.Application.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IPhotoService> _photoService;


        public ServiceManager
        (
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            IOptions<JwtConfiguration> configuration,
            Cloudinary cloudinary
        )
        {
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration));
            _photoService = new Lazy<IPhotoService>(() => new PhotoService(cloudinary));
    }
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public IPhotoService PhotoService => _photoService.Value;
    }
}
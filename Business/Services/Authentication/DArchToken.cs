﻿using Core.Utilities.Security.Jwt;
using Core.Entities.Concrete;

namespace Business.Services.Authentication
{
    public class DArchToken : AccessToken
    {
        public string ExternalUserId { get; set; }
        /// <summary>
        /// AuthenticationProviderType'a karşılık gelir.
        /// </summary>
        public AuthenticationProviderType Provider { get; set; }
        /// <summary>
        /// Eğer bu özellik dolu dönerse login olan kullanıcı bilgisi yanında aşağıdaki
        /// bağışçı alınmış gibi varsayılabilir.
        /// </summary>
        public string OnBehalfOf { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoFrame.Domain.Model;

namespace PhotoFrame.Domain.UseCase
{
    public class ToggleFavorite
    {
        private readonly IPhotoRepository photoRepository;

        public ToggleFavorite(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        /// <summary>
        /// 引数で渡したフォトのお気に入りON/OFFを切り替えて返す
        /// ※参照渡しなので返り値を保存しなくても書き換わる
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public Photo Execute(Photo photo)
        {
           if(photo.IsFavorite == true)
            {
                photo.MarkAsUnFavorite();
            }
            else
            {
                photo.MarkAsFavorite();
            }

            photoRepository.Store(photo);
            

            return photo;
        }

        public async Task<Photo> ExecuteAsync(Photo photo)
        {
            if (photo.IsFavorite == true)
            {
                photo.MarkAsUnFavorite();
            }
            else
            {
                photo.MarkAsFavorite();
            }

            await Task.Run(() =>
            {
                photoRepository.Store(photo);
            });
            
            return photo;
        }
    }
}

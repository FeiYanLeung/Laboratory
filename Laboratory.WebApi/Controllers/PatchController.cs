using JsonPatch;
using Laboratory.WebApi.Models.Patch;
using System.Collections.Generic;
using System.Web.Http;

namespace Laboratory.WebApi.Controllers
{
    /// <summary>
    /// JsonPatch
    /// <seealso cref="http://tools.ietf.org/html/rfc6902"/>
    /// </summary>
    [RoutePrefix("api/patch")]
    public class PatchController : ApiController
    {
        private readonly List<Album> albums;

        public PatchController()
        {
            this.albums = new List<Album>()
            {
                new Album() {
                    AlbumId = 1,
                    Title = "The Best Of Men At Work",
                    Price = 9.99m,
                    Tags = new List<string>(){
                        "女毒",
                        "摇滚"
                    },
                    IsDeleted = 0,
                    Genre = new Genre(){
                        GenreId = 1,
                        Name ="Rock"
                    }
                },
                new Album() {
                    AlbumId = 2,
                    Title = "The Angel Sound",
                    Price = 0.99m,
                    Tags = new List<string>(){
                        "儿童",
                        "合唱",
                        "古典"
                    },
                    IsDeleted = 0,
                    Genre = new Genre(){
                        GenreId =2,
                        Name ="Classic"
                    }
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Album> Get()
        {
            return this.albums;
        }

        /// <summary>
        /// ADD
        /// </summary>
        /// <param name="patchData"></param>
        /// <returns></returns>
        public List<Album> Add(JsonPatchDocument<Album> patchData)
        {
            /*
            /api/patch/add
            [
                { "op": "add", "path": "/AlbumId", "value": 3},
                { "op": "add", "path": "/Title", "value": 'Chinese'},
                { "op": "add", "path": "/Price", "value": '9.99'},
                { "op": "add", "path": "/Tags", "value": ['限免','粤语']},
                { "op": "add", "path": "/IsDeleted", "value": '0'},
                { "op": "add", "path": "/Genre", "value": {"GenreId": 1, "Name": "Men At Work"}}
            ]
            */

            var album = new Album();
            patchData.ApplyUpdatesTo(album);
            this.albums.Add(album);

            return this.albums;
        }


        /// <summary>
        /// REMOVE
        /// </summary>
        /// <param name="patchData"></param>
        /// <returns></returns>
        public List<Album> Remove(int albumId, JsonPatchDocument<Album> patchData)
        {
            /*
             /api/patch/remove?albumId=2
             [
                { "op": "remove", "path": "/Tags/0"}
             ]
             */

            var album = this.albums.Find(w => w.AlbumId == albumId);
            patchData.ApplyUpdatesTo(album);

            return this.albums;
        }

        /// <summary>
        /// REPLACE
        /// </summary>
        /// <param name="patchData"></param>
        /// <returns></returns>
        public List<Album> Replace(int albumId, JsonPatchDocument<Album> patchData)
        {
            /*
            /api/patch/replace?albumId=2
            [
                { "op": "replace", "path": "/Tags", value:["1","2","3"] },
                { "op": "replace", "path": "/Tags/0", value:"再次替换" }
            ]
            */
            var album = this.albums.Find(w => w.AlbumId == albumId);
            patchData.ApplyUpdatesTo(album);

            return this.albums;
        }

        /// <summary>
        /// MOVE
        /// </summary>
        /// <param name="patchData"></param>
        /// <returns></returns>
        public List<Album> Move(int albumId, JsonPatchDocument<Album> patchData)
        {
            /*
            /api/patch/move?albumId=1
            [
                { "op": "move", "from": "/Tags/0", "path": "/Genre/Description" }
            ]
            */

            var album = this.albums.Find(w => w.AlbumId == albumId);
            patchData.ApplyUpdatesTo(album);

            return this.albums;
        }
    }
}

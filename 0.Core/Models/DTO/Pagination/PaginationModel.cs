namespace Core.Models.DTO.Pagination
{
    /// <summary>
    /// 分頁參數Model
    /// </summary>
    public class PaginationModel
    {
        /// <summary>
        /// 略過的資料數
        /// </summary>
        public int skipCount { get; set; }
        /// <summary>
        /// 每頁的資料數
        /// </summary>
        public int limitCount { get; set; }
    }
}
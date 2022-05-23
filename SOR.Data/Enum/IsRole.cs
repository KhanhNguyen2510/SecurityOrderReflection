namespace SOR.Data.Enum
{
    public enum IsRole
    {
        /// <summary>
        /// Không có quyền nào hết
        /// </summary>
        None,
        /// <summary>
        /// Chỉ được xem
        /// </summary>
        ReadOnly,
        /// <summary>
        /// Chỉnh sửa
        /// </summary>
        Edit,
        /// <summary>
        /// Có full quyền
        /// </summary>
        Full
    }
}

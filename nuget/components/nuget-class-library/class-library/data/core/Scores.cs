namespace nuget_class_library.class_library.data.core
{
    /// <summary>
    /// Holds data concerning game scores for a customer object.
    /// </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="Scores"/> class.
    /// </remarks>
    /// <param name="referenceNumber">The reference number to set.</param>
    /// <param name="ytdTotalScoreValue">The ytd total score value to set.</param>
    public class Scores(
            string referenceNumber,
            string ytdTotalScoreValue)
    {
        /// <summary>
        /// Gets the ReferenceNumber.
        /// </summary>
        public string ReferenceNumber { get; private set; } = referenceNumber;

        /// <summary>
        /// Gets the YTDScoreValue.
        /// </summary>
        public string YTDTotalScoreValue { get; private set; } = ytdTotalScoreValue;

        /// <summary>
        /// Gets the RecordUpdatedDate; the date/time the row was last updated.
        /// </summary>
        public string RecordUpdatedDate { get; private set; } = UnitHelper.DateTimeToIsoString(DateTime.Now);
    }
}

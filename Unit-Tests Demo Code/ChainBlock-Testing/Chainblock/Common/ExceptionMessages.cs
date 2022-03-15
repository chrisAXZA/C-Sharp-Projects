using System;
namespace Chainblock.Common
{
    public static class ExceptionMessages
    {
        public static string InvalidIdMessage =
            "ID can not be zero or negative!";

        public static string InvalidSenderUsernameMessage =
            "Sender-name can not be whitespace or empty!";

        public static string InvalidReceiverUsernameMessage =
            "Receiver-name can not be whitespace or empty!";

        public static string InvalidTransactionAmountMessage =
            "Transaction amount should be greater than zero!";

        public static string InvalidAddExistingTransactionMessage =
            "Transaction already exists in records!";

        public static string ChangeStatusOfNonExistingTransactionMessage =
            "Status of non-existing transaction can not be changed!";

        public static string GetIdOfNonExistingTransactionMessage =
            "Transaction with given ID does not exist!";

        public static string RemoveNonExistingTransactionMessage =
            "Non-existant transaction can not be removed!";

        public static string NonExistingTransactionWithGivenStatusMessage =
            "There is no transaction with given status in the repository!";

        public static string GetAllSendersWithTransactionStatusNonExistingStatusMessage =
            "There is no sender in the repositiory with given transaction status!";

        public static string GetBySenderOrderedByAmountDescendingNonExistingSenderMessage =
            "There are no transactions in the repositiory with given sender!";

        public static string GetByReceiverOrderedByAmountThenByIdNonExistingReceiverMessage =
            "There are no transactions in the repositiory with given receiver!";

        public static string GetBySenderAndMinimumAmountDescendingNonExistingTransactionMessage =
            "There are no transactions in the repositiory with given sender!";

        public static string GetByReceiverAndAmountRangeNonExistingTransactionMessage =
            "There are no transactions in the repositiory with given receiver or amount ranges!";
    }
}

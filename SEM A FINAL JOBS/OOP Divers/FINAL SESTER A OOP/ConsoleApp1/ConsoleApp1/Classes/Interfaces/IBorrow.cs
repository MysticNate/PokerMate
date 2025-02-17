/// <summary>
/// Because not all items are available, we will need to borrow items from the clubs
/// </summary>
interface IBorrow
{
    // Will be used on divers to borrow items
    public void BorrowItem(Diver diver, Item item, DivingClub divingClub);
    public void ReturnItem(Diver diver, Item item, DivingClub divingClub);
}
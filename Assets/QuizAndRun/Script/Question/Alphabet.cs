public static class Alphabet
{
    private static string alphabet = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
    public static char GetCharByIndex(int _index)
    {
        if(_index >= 0 && _index < alphabet.Length)
        {
            return alphabet[_index];
        }
        return '\0';
    }
}

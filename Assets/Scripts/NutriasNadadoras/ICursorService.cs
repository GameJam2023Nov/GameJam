internal interface ICursorService
{
    void StateOfCursor(bool canEvaluate);
    void StateOfCursorEvaluator(bool isGood);
    void StateOfRelease(bool isRelease);
}
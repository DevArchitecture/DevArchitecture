using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            return logics.FirstOrDefault(result => result is { Success: false });
        }

        public static List<IResult> RunMultiple(params IResult[] logics)
        {
            return logics.Where(logic => !logic.Success).ToList();
        }
    }
}

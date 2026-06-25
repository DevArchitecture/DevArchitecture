# PaginatedResult Kullanım Örneği

Bu doküman `PaginatedResult<T>`'in nasıl kullanılacağını gösterir.

## Controller'da Kullanım

```csharp
[HttpGet]
public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
{
    var result = await Mediator.Send(new GetUsersQuery(pageNumber, pageSize));
    return result.Success ? Ok(result.Data) : BadRequest(result.Message);
}
```

## Query Handler'da Kullanım

```csharp
public class GetUsersQuery : BasePaginatedQuery<UserDto>
{
    public GetUsersQuery(int pageNumber = 1, int pageSize = 10) : base(pageNumber, pageSize) { }

    public class Handler : IRequestHandler<GetUsersQuery, IDataResult<PaginatedResult<IEnumerable<UserDto>>>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public Handler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IDataResult<PaginatedResult<IEnumerable<UserDto>>>> Handle(
            GetUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetListAsync();
            var total = list.Count();

            var paged = list
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => _mapper.Map<UserDto>(x))
                .ToList();

            var result = new PaginatedResult<IEnumerable<UserDto>>(paged, request.PageNumber, request.PageSize)
            {
                TotalRecords = total,
                TotalPages = (int)Math.Ceiling(total / (double)request.PageSize)
            };

            return new SuccessDataResult<PaginatedResult<IEnumerable<UserDto>>>(result);
        }
    }
}
```

## API Response Formatı

```json
{
  "success": true,
  "message": "List was paginated successfully.",
  "data": [ ... ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 5,
  "totalRecords": 42,
  "firstPage": "https://localhost:5101/api/v1/users?pageNumber=1&pageSize=10",
  "lastPage": "https://localhost:5101/api/v1/users?pageNumber=5&pageSize=10",
  "nextPage": "https://localhost:5101/api/v1/users?pageNumber=2&pageSize=10",
  "previousPage": null
}
```

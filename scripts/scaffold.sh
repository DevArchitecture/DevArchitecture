#!/usr/bin/env bash
# DevArchitecture Scaffolding Tool
# Usage: ./scripts/scaffold.sh <EntityName> [properties...]
# Example: ./scripts/scaffold.sh Product Name:string Price:decimal CategoryId:int
#
# This generates: Entity, DTO, Repository interface, CRUD handlers, Controller, Tests

set -euo pipefail

if [ $# -lt 1 ]; then
  echo "Usage: $0 <EntityName> [Property:Type...]"
  echo ""
  echo "Example:"
  echo "  $0 Product Name:string Price:decimal CategoryId:int"
  echo ""
  echo "This will generate:"
  echo "  - Entities/Concrete/<Entity>.cs"
  echo "  - Core/Entities/Dtos/<Entity>Dto.cs"
  echo "  - DataAccess/Abstract/I<Entity>Repository.cs"
  echo "  - Business/Handlers/<Entities>/Queries/"
  echo "  - Business/Handlers/<Entities>/Commands/"
  echo "  - WebAPI/Controllers/<Entities>Controller.cs"
  echo "  - Tests/Business/Handlers/<Entities>HandlerTests.cs"
  exit 1
fi

NAME="$1"
shift
PROPS=("$@")
LOWER="$(echo "$NAME" | tr '[:upper:]' '[:lower:]')"

# Basic English pluralization
case "$LOWER" in
  *category) PLURAL="${LOWER}ies" ;;
  *y) PLURAL="${LOWER%y}ies" ;;
  *s|*x|*ch|*sh) PLURAL="${LOWER}es" ;;
  *) PLURAL="${LOWER}s" ;;
esac

ROOT="$(cd "$(dirname "$0")/.." && pwd)"

# Default properties if none provided
if [ ${#PROPS[@]} -eq 0 ]; then
  PROPS=("Name:string")
fi

# Build property strings
CS_PROPS=""
DTO_PROPS=""
# Primary key is always Id:int
ALL_PROPS="Id:int ${PROPS[@]}"

# Generate Entity
ENTITY_FILE="$ROOT/Entities/Concrete/${NAME}.cs"
if [ ! -f "$ENTITY_FILE" ]; then
  echo "  Creating Entity: Entities/Concrete/${NAME}.cs"
  cat > "$ENTITY_FILE" << ENTEOF
using Core.Entities;

namespace Entities.Concrete
{
    public class $NAME : IEntity
    {
        public int Id { get; set; }
$(for prop in "${PROPS[@]}"; do
  pname="${prop%%:*}"
  ptype="${prop##*:}"
  echo "        public $ptype $pname { get; set; }"
done)
    }
}
ENTEOF
else
  echo "  SKIP: $ENTITY_FILE already exists"
fi

# Generate DTO
DTO_FILE="$ROOT/Core/Entities/Dtos/${NAME}Dto.cs"
if [ ! -f "$DTO_FILE" ]; then
  echo "  Creating DTO: Core/Entities/Dtos/${NAME}Dto.cs"
  cat > "$DTO_FILE" <<DTOEOF
using Core.Entities;

namespace Core.Entities.Dtos
{
    public class ${NAME}Dto : IEntity
    {
        public int Id { get; set; }
$(for prop in "${PROPS[@]}"; do
  pname="${prop%%:*}"
  ptype="${prop##*:}"
  echo "        public $ptype $pname { get; set; }"
done)
    }
}
DTOEOF
else
  echo "  SKIP: $DTO_FILE already exists"
fi

# Generate Repository Interface
REPO_FILE="$ROOT/DataAccess/Abstract/I${NAME}Repository.cs"
if [ ! -f "$REPO_FILE" ]; then
  echo "  Creating Repository: DataAccess/Abstract/I${NAME}Repository.cs"
  cat > "$REPO_FILE" <<REPOEOF
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface I${NAME}Repository : IEntityRepository<$NAME>
    {
    }
}
REPOEOF
else
  echo "  SKIP: $REPO_FILE already exists"
fi

# Create handler directories
mkdir -p "$ROOT/Business/Handlers/${PLURAL}/Queries"
mkdir -p "$ROOT/Business/Handlers/${PLURAL}/Commands"

# Generate Get${NAME}sQuery
QUERY_FILE="$ROOT/Business/Handlers/${PLURAL}/Queries/Get${NAME}sQuery.cs"
if [ ! -f "$QUERY_FILE" ]; then
  echo "  Creating Query: Business/Handlers/${PLURAL}/Queries/Get${NAME}sQuery.cs"
  cat > "$QUERY_FILE" <<QRYEOF
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.${PLURAL}.Queries
{
    public class Get${NAME}sQuery : IRequest<IDataResult<IEnumerable<${NAME}Dto>>>
    {
        public class Get${NAME}sQueryHandler : IRequestHandler<Get${NAME}sQuery, IDataResult<IEnumerable<${NAME}Dto>>>
        {
            private readonly I${NAME}Repository _${LOWER}Repository;
            private readonly IMapper _mapper;

            public Get${NAME}sQueryHandler(I${NAME}Repository ${LOWER}Repository, IMapper mapper)
            {
                _${LOWER}Repository = ${LOWER}Repository;
                _mapper = mapper;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<${NAME}Dto>>> Handle(Get${NAME}sQuery request, CancellationToken cancellationToken)
            {
                var list = await _${LOWER}Repository.GetListAsync();
                var dtoList = list.Select(e => _mapper.Map<${NAME}Dto>(e)).ToList();
                return new SuccessDataResult<IEnumerable<${NAME}Dto>>(dtoList);
            }
        }
    }
}
QRYEOF
else
  echo "  SKIP: $QUERY_FILE already exists"
fi

# Generate Get${NAME}Query
GET_FILE="$ROOT/Business/Handlers/${PLURAL}/Queries/Get${NAME}Query.cs"
if [ ! -f "$GET_FILE" ]; then
  echo "  Creating Query: Business/Handlers/${PLURAL}/Queries/Get${NAME}Query.cs"
  cat > "$GET_FILE" <<GETEOF
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.${PLURAL}.Queries
{
    public class Get${NAME}Query : IRequest<IDataResult<${NAME}Dto>>
    {
        public int Id { get; set; }

        public class Get${NAME}QueryHandler : IRequestHandler<Get${NAME}Query, IDataResult<${NAME}Dto>>
        {
            private readonly I${NAME}Repository _${LOWER}Repository;
            private readonly IMapper _mapper;

            public Get${NAME}QueryHandler(I${NAME}Repository ${LOWER}Repository, IMapper mapper)
            {
                _${LOWER}Repository = ${LOWER}Repository;
                _mapper = mapper;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<${NAME}Dto>> Handle(Get${NAME}Query request, CancellationToken cancellationToken)
            {
                var item = await _${LOWER}Repository.GetAsync(p => p.Id == request.Id);
                var dto = _mapper.Map<${NAME}Dto>(item);
                return new SuccessDataResult<${NAME}Dto>(dto);
            }
        }
    }
}
GETEOF
else
  echo "  SKIP: $GET_FILE already exists"
fi

# Generate Create${NAME}Command
CREATE_FILE="$ROOT/Business/Handlers/${PLURAL}/Commands/Create${NAME}Command.cs"
if [ ! -f "$CREATE_FILE" ]; then
  echo "  Creating Command: Business/Handlers/${PLURAL}/Commands/Create${NAME}Command.cs"
  cat > "$CREATE_FILE" <<CRTEOF
using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.${PLURAL}.Commands
{
    public class Create${NAME}Command : IRequest<IResult>
    {
$(for prop in "${PROPS[@]}"; do
  pname="${prop%%:*}"
  ptype="${prop##*:}"
  echo "        public $ptype $pname { get; set; }"
done)

        public class Create${NAME}CommandHandler : IRequestHandler<Create${NAME}Command, IResult>
        {
            private readonly I${NAME}Repository _${LOWER}Repository;

            public Create${NAME}CommandHandler(I${NAME}Repository ${LOWER}Repository)
            {
                _${LOWER}Repository = ${LOWER}Repository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(Create${NAME}Command request, CancellationToken cancellationToken)
            {
                var entity = new Entities.Concrete.$NAME
                {
$(for prop in "${PROPS[@]}"; do
  pname="${prop%%:*}"
  echo "                    $pname = request.$pname,"
done)
                };

                _${LOWER}Repository.Add(entity);
                await _${LOWER}Repository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
CRTEOF
else
  echo "  SKIP: $CREATE_FILE already exists"
fi

# Generate Update${NAME}Command
UPDATE_FILE="$ROOT/Business/Handlers/${PLURAL}/Commands/Update${NAME}Command.cs"
if [ ! -f "$UPDATE_FILE" ]; then
  echo "  Creating Command: Business/Handlers/${PLURAL}/Commands/Update${NAME}Command.cs"
  cat > "$UPDATE_FILE" <<UPDEOF
using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.${PLURAL}.Commands
{
    public class Update${NAME}Command : IRequest<IResult>
    {
        public int Id { get; set; }
$(for prop in "${PROPS[@]}"; do
  pname="${prop%%:*}"
  ptype="${prop##*:}"
  echo "        public $ptype $pname { get; set; }"
done)

        public class Update${NAME}CommandHandler : IRequestHandler<Update${NAME}Command, IResult>
        {
            private readonly I${NAME}Repository _${LOWER}Repository;

            public Update${NAME}CommandHandler(I${NAME}Repository ${LOWER}Repository)
            {
                _${LOWER}Repository = ${LOWER}Repository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(Update${NAME}Command request, CancellationToken cancellationToken)
            {
                var entity = await _${LOWER}Repository.GetAsync(p => p.Id == request.Id);
                if (entity == null)
                    return new ErrorResult(Messages.IdNotFound);

$(for prop in "${PROPS[@]}"; do
  pname="${prop%%:*}"
  echo "                entity.$pname = request.$pname;"
done)

                _${LOWER}Repository.Update(entity);
                await _${LOWER}Repository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
UPDEOF
else
  echo "  SKIP: $UPDATE_FILE already exists"
fi

# Generate Delete${NAME}Command
DELETE_FILE="$ROOT/Business/Handlers/${PLURAL}/Commands/Delete${NAME}Command.cs"
if [ ! -f "$DELETE_FILE" ]; then
  echo "  Creating Command: Business/Handlers/${PLURAL}/Commands/Delete${NAME}Command.cs"
  cat > "$DELETE_FILE" <<DELEOF
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.${PLURAL}.Commands
{
    public class Delete${NAME}Command : IRequest<IResult>
    {
        public int Id { get; set; }

        public class Delete${NAME}CommandHandler : IRequestHandler<Delete${NAME}Command, IResult>
        {
            private readonly I${NAME}Repository _${LOWER}Repository;

            public Delete${NAME}CommandHandler(I${NAME}Repository ${LOWER}Repository)
            {
                _${LOWER}Repository = ${LOWER}Repository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(Delete${NAME}Command request, CancellationToken cancellationToken)
            {
                var entity = await _${LOWER}Repository.GetAsync(p => p.Id == request.Id);
                if (entity == null)
                    return new ErrorResult(Messages.IdNotFound);

                _${LOWER}Repository.Delete(entity);
                await _${LOWER}Repository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
DELEOF
else
  echo "  SKIP: $DELETE_FILE already exists"
fi

# Generate Controller
CTRL_FILE="$ROOT/WebAPI/Controllers/${PLURAL}Controller.cs"
if [ ! -f "$CTRL_FILE" ]; then
  echo "  Creating Controller: WebAPI/Controllers/${PLURAL}Controller.cs"
  cat > "$CTRL_FILE" <<CTREOF
using Business.Handlers.${PLURAL}.Commands;
using Business.Handlers.${PLURAL}.Queries;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/$PLURAL")]
    [ApiController]
    public class ${PLURAL}Controller : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new Get${NAME}sQuery());
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new Get${NAME}Query { Id = id });
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Create${NAME}Command command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Created("", result.Message) : BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Update${NAME}Command command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new Delete${NAME}Command { Id = id });
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
CTREOF
else
  echo "  SKIP: $CTRL_FILE already exists"
fi

# Generate Tests
TEST_FILE="$ROOT/Tests/Business/Handlers/${NAME}HandlerTests.cs"
if [ ! -f "$TEST_FILE" ]; then
  echo "  Creating Tests: Tests/Business/Handlers/${NAME}HandlerTests.cs"
  cat > "$TEST_FILE" <<TESTEOF
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Handlers.${PLURAL}.Commands;
using Business.Handlers.${PLURAL}.Queries;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ${NAME}HandlerTests
    {
        private Mock<I${NAME}Repository> _repository;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<I${NAME}Repository>();
            _mapper = new Mock<IMapper>();

            _mapper.Setup(x => x.Map<${NAME}Dto>(It.IsAny<$NAME>()))
                .Returns(($NAME e) => new ${NAME}Dto { Id = e.Id });
        }

        [Test]
        public async Task Get${NAME}sQuery_ReturnsAll()
        {
            var items = new List<$NAME>
            {
                new $NAME { Id = 1 },
                new $NAME { Id = 2 }
            }.AsQueryable();
            _repository.Setup(x => x.GetListAsync()).ReturnsAsync(items);

            var handler = new Get${NAME}sQuery.Get${NAME}sQueryHandler(_repository.Object, _mapper.Object);
            var result = await handler.Handle(new Get${NAME}sQuery(), CancellationToken.None);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Test]
        public async Task Get${NAME}Query_ReturnsById()
        {
            var item = new $NAME { Id = 1 };
            _repository.Setup(x => x.GetAsync(It.IsAny<Expression<System.Func<$NAME, bool>>>())).ReturnsAsync(item);

            var handler = new Get${NAME}Query.Get${NAME}QueryHandler(_repository.Object, _mapper.Object);
            var result = await handler.Handle(new Get${NAME}Query { Id = 1 }, CancellationToken.None);

            result.Success.Should().BeTrue();
        }

        [Test]
        public async Task Create${NAME}Command_Creates()
        {
            _repository.Setup(x => x.Add(It.IsAny<$NAME>()));
            _repository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var handler = new Create${NAME}Command.Create${NAME}CommandHandler(_repository.Object);
            var result = await handler.Handle(new Create${NAME}Command { }, CancellationToken.None);

            result.Success.Should().BeTrue();
        }
    }
}
TESTEOF
else
  echo "  SKIP: $TEST_FILE already exists"
fi

echo ""
echo "✅ Scaffolding complete for: $NAME"
echo ""
echo "Generated files:"
[ -f "$ENTITY_FILE" ] && echo "  - Entities/Concrete/${NAME}.cs"
[ -f "$DTO_FILE" ] && echo "  - Core/Entities/Dtos/${NAME}Dto.cs"
[ -f "$REPO_FILE" ] && echo "  - DataAccess/Abstract/I${NAME}Repository.cs"
[ -f "$QUERY_FILE" ] && echo "  - Business/Handlers/${PLURAL}/Queries/Get${NAME}sQuery.cs"
[ -f "$GET_FILE" ] && echo "  - Business/Handlers/${PLURAL}/Queries/Get${NAME}Query.cs"
[ -f "$CREATE_FILE" ] && echo "  - Business/Handlers/${PLURAL}/Commands/Create${NAME}Command.cs"
[ -f "$UPDATE_FILE" ] && echo "  - Business/Handlers/${PLURAL}/Commands/Update${NAME}Command.cs"
[ -f "$DELETE_FILE" ] && echo "  - Business/Handlers/${PLURAL}/Commands/Delete${NAME}Command.cs"
[ -f "$CTRL_FILE" ] && echo "  - WebAPI/Controllers/${PLURAL}Controller.cs"
[ -f "$TEST_FILE" ] && echo "  - Tests/Business/Handlers/${NAME}HandlerTests.cs"
echo ""
echo "Next steps:"
echo "  1. Add EntityTypeConfiguration in DataAccess/Concrete/Configurations/"
echo "  2. Add DbSet in DataAccess/Concrete/Contexts/"
echo "  3. Register repository in Business/Startup.cs"
echo "  4. Run: dotnet test"

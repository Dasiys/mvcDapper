using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expression
{
    class Program
    {
        static void Main(string[] args)
        {
            var dtoUser = GetExpression()(new User() {Name = "叶伟密"});
            Console.WriteLine(dtoUser.Name);
            Console.ReadLine();
        }

        static Func<User, UserDto> GetExpression()
        {
            ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(User), "c");
            MemberExpression mem = System.Linq.Expressions.Expression.Property(param, "Name");

            var loc = System.Linq.Expressions.Expression.Variable(typeof(UserDto), "dto");
            NewExpression newExpression = System.Linq.Expressions.Expression.New(typeof(UserDto));
            BinaryExpression newDto = System.Linq.Expressions.Expression.Assign(loc, newExpression);

            MemberExpression meml = System.Linq.Expressions.Expression.Property(loc, "Name");
            BinaryExpression asn = System.Linq.Expressions.Expression.Assign(meml, mem);

            LabelTarget labelTarget = System.Linq.Expressions.Expression.Label(typeof(UserDto));
            GotoExpression gt = System.Linq.Expressions.Expression.Return(labelTarget, loc);
            LabelExpression lb = System.Linq.Expressions.Expression.Label(labelTarget,System.Linq.Expressions.Expression.Constant(new UserDto() { Name = "" }));

            BlockExpression blocks = System.Linq.Expressions.Expression.Block(new[] { loc }, loc,
                newDto, asn, gt,lb);
            Expression<Func<User, UserDto>> dt = System.Linq.Expressions.Expression.Lambda<Func<User, UserDto>>(blocks, param);
            return dt.Compile();
        }
    }
}

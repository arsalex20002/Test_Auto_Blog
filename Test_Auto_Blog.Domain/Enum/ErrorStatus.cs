using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Test_Auto_Blog.Domain.Enum
{
	public enum ErrorStatus
	{
		CarNotFound = 0,
        UserNotFound = 1,
        PostNotFound = 2,
        NoPostsByFilter =3,
		Success = 200,
		InternalServerError = 500,

		

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        /*

                        For example, let's write the following aspect code for any method in BrandManager.

            [ValidationAspect(typeof(BrandValidator))]
                              ----------------------
                                        |
                                  BrandValidator

        */

        public ValidationAspect(Type validatorType)
        {
            // Checks if the sent Validator (BrandValidator) is of type IValidator.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {

                // Otherwise it throws an error.
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            // If it is of type IValidator, it does the assignment. (_validatorType = BrandValidator)
            _validatorType = validatorType;
        }

        // OnBefore = Activates the relevant aspect before the method runs.
        protected override void OnBefore(IInvocation invocation)
        {

            // Creates an instance from Validator (BrandValidator).
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

            // Gets the value (Brand) of the Generic argument in the Validator's base class (AbstractValidator<Brand>).
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            // Find the entities that are instanced from the entityType class (Brand) in the method's parameters. (Ex: Brand brand)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            // Go through them all and verify that they meet the conditions in the Validator (BrandValidator).
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
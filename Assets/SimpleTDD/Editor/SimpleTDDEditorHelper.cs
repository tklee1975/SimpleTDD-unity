﻿using System;
using System.Reflection;

namespace SimpleTDD 
{
	public class SimpleTDDEditorHelper
	{
		public static Type GetType(string typeName)
		{

			// Try Type.GetType() first. This will work with types defined
			// by the Mono runtime, in the same assembly as the caller, etc.
			Type type = Type.GetType(typeName);

			// If it worked, then we're done here
			if( type != null ) {
				return type;
			}

			// If the TypeName is a full name, then we can try loading the defining assembly directly
			if( typeName.Contains(".") ) {

				// Get the name of the assembly (Assumption is that we are using 
				// fully-qualified type names)
				var assemblyName = typeName.Substring( 0, typeName.IndexOf( '.' ) );

				// Attempt to load the indicated Assembly
				var assembly = Assembly.Load( assemblyName );
				if( assembly == null ) {
					return null;
				}

				// Ask that assembly to return the proper Type
				type = assembly.GetType( typeName );
				if( type != null ) {
					return type;
				}

			}

			// If we still haven't found the proper type, we can enumerate all of the 
			// loaded assemblies and see if any of them define the type
			Assembly currentAssembly = Assembly.GetExecutingAssembly();
			AssemblyName[] referencedAssemblies = currentAssembly.GetReferencedAssemblies();
			foreach(AssemblyName assemblyName in referencedAssemblies ) {

				// Load the referenced assembly
				Assembly assembly = Assembly.Load( assemblyName );
				if( assembly != null )
				{
					// See if that assembly defines the named type
					type = assembly.GetType(typeName);
					if( type != null )
						return type;
				}
			}

			// The type just couldn't be found...
			return null;

		}
	}
}

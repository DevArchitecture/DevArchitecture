﻿using MongoDB.Bson;

namespace Core.Entities
{
	public abstract class DocumentDbEntity
	{
		public ObjectId Id { get; set; }
	}
}
